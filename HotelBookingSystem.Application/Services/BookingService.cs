using AutoMapper;
using HotelBookingSystem.Application.DTO.BookingDTO;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces.Repository;
using HotelBookingSystem.Infrastructure.EmailSender;
using HotelBookingSystem.Infrastructure.PdfGenerator;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;


namespace HotelBookingSystem.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IPdfService _pdfService;

        public BookingService(
            IBookingRepository bookingRepository,
            IRoomRepository roomRepository,
            IHotelRepository hotelRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ICityRepository cityRepository,
            IEmailService emailService,
            IPdfService pdfService)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _cityRepository = cityRepository;
            _emailService = emailService;
            _pdfService = pdfService;
        }

        public async Task<BookingResponse> CreateBookingAsync(BookingRequest bookingRequest)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException("User not found"));

            var room = await _roomRepository.GetByIdAsync(bookingRequest.RoomId);
            if (room == null) throw new KeyNotFoundException("Room not found");

            var hotel = await _hotelRepository.GetByIdAsync(bookingRequest.HotelId);
            if (hotel == null) throw new KeyNotFoundException("Hotel not found");


            var checkInDate = bookingRequest.CheckInDate;
            var checkOutDate = bookingRequest.CheckOutDate;

            var isRoomAvailable = room.Bookings.All(b => b.CheckOutDate <= checkInDate || b.CheckInDate >= checkOutDate);
            if (!isRoomAvailable) throw new InvalidOperationException("Room is already booked for the specified dates");

            decimal totalPrice = CalculateTotalPrice(room, checkInDate, checkOutDate);

            var booking = _mapper.Map<Booking>(bookingRequest);

            booking.TotalPrice = totalPrice;
            booking.UserId = userId;
            booking.User = await _userRepository.GetByIdAsync(userId);

            await _bookingRepository.AddAsync(booking);

            var city = await _cityRepository.GetByIdAsync(hotel.CityId);
            if (city != null)
            {
                city.Visitors++;
                await _cityRepository.UpdateAsync(city);
            }

            var bookingResponse = _mapper.Map<BookingResponse>(booking);

            SendEmailWithBookingDetails(booking, bookingResponse);

            return bookingResponse;
        }

        private void SendEmailWithBookingDetails(Booking booking, BookingResponse bookingResponse)
        {
            string emailSubject = "Your booking details";
            string emailBody = GenerateBookingEmailBody(bookingResponse);
            _emailService.SendEmailAsync(booking.User.Email, emailSubject, emailBody);
        }

        private static decimal CalculateTotalPrice(Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            var totalNights = (checkOutDate - checkInDate).Days;
            var pricePerNight = room.FeaturedDeal && room.DiscountedPrice.HasValue
                ? room.DiscountedPrice.Value
                : room.PricePerNight;
            var totalPrice = totalNights * pricePerNight;
            return totalPrice;
        }

        private string GenerateBookingEmailBody(BookingResponse bookingResponse)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<h1>Booking Details</h1>");
            sb.AppendLine($"<p><strong>Booking number:</strong> {bookingResponse.BookingId}</p>");
            sb.AppendLine($"<p><strong>User Name:</strong> {bookingResponse.UserFirstName} {bookingResponse.UserLastName}</p>");
            sb.AppendLine($"<p><strong>Hotel Name:</strong> {bookingResponse.HotelName}</p>");
            sb.AppendLine($"<p><strong>Hotel Address:</strong> {bookingResponse.HotelAddress}</p>");
            sb.AppendLine($"<p><strong>Room Type:</strong> {bookingResponse.RoomType}</p>");
            sb.AppendLine($"<p><strong>Special Requests:</strong> {bookingResponse.SpecialRequests}</p>");
            sb.AppendLine($"<p><strong>Check-in Date:</strong> {bookingResponse.CheckInDate.ToString("d")}</p>");
            sb.AppendLine($"<p><strong>Check-out Date:</strong> {bookingResponse.CheckOutDate.ToString("d")}</p>");
            sb.AppendLine($"<p><strong>Total Price:</strong> ${bookingResponse.TotalPrice}</p>");

            if (bookingResponse.Payment != null)
            {
                sb.AppendLine("<h2>Payment Details</h2>");
                sb.AppendLine($"<p><strong>Payment number:</strong> {bookingResponse.Payment.PaymentId}</p>");
                sb.AppendLine($"<p><strong>Amount:</strong> ${bookingResponse.Payment.Amount}</p>");
                sb.AppendLine($"<p><strong>Payment Date:</strong> {bookingResponse.Payment.PaymentDate.ToString("d")}</p>");
                sb.AppendLine($"<p><strong>Payment Method:</strong> {bookingResponse.Payment.PaymentMethod}</p>");
                sb.AppendLine($"<p><strong>Payment Status:</strong> {bookingResponse.Payment.Status}</p>");
            }

            return sb.ToString();
        }

        public async Task CancelBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) throw new KeyNotFoundException("Booking not found");

            var city = await _cityRepository.GetByIdAsync(booking.Hotel.CityId);
            if (city != null)
            {
                city.Visitors--;
                await _cityRepository.UpdateAsync(city);
            }
            _bookingRepository.DeleteAsync(bookingId);
        }

        public async Task<BookingResponse> UpdateBookingAsync(int bookingId, BookingRequest bookingRequest)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) throw new KeyNotFoundException("Booking not found");

            var room = await _roomRepository.GetByIdAsync(bookingRequest.RoomId);
            if (room == null) throw new KeyNotFoundException("Room not found");

            var hotel = await _hotelRepository.GetByIdAsync(bookingRequest.HotelId);
            if (hotel == null) throw new KeyNotFoundException("Hotel not found");


            var checkInDate = bookingRequest.CheckInDate;
            var checkOutDate = bookingRequest.CheckOutDate;
            var totalNights = (checkOutDate - checkInDate).Days;
            var totalPrice = totalNights * room.PricePerNight;

            booking = _mapper.Map<Booking>(bookingRequest);
            booking.TotalPrice = totalPrice;

            await _bookingRepository.UpdateAsync(booking);

            return _mapper.Map<BookingResponse>(booking);
        }


        public async Task<BookingResponse> GetBookingDetailsAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) throw new KeyNotFoundException("Booking not found");

            var bookingResponse = _mapper.Map<BookingResponse>(booking);

            return bookingResponse;
        }

        public async Task<byte[]> GetBookingPdfAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) throw new KeyNotFoundException("Booking not found");

            var bookingResponse = _mapper.Map<BookingResponse>(booking);

            byte[] bookingPdf = _pdfService.GenerateBookingPdf(bookingResponse);

            return bookingPdf;
        }


    }

}
