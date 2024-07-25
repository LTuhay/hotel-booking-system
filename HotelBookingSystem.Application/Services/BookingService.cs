using AutoMapper;
using HotelBookingSystem.Application.DTO.BookingDTO;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace HotelBookingSystem.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingService(
            IBookingRepository bookingRepository,
            IRoomRepository roomRepository,
            IHotelRepository hotelRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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
            var totalNights = (checkOutDate - checkInDate).Days;
            var pricePerNight = room.FeaturedDeal && room.DiscountedPrice.HasValue
                ? room.DiscountedPrice.Value
                : room.PricePerNight;
            var totalPrice = totalNights * pricePerNight;
            var booking = _mapper.Map<Booking>(bookingRequest);
            booking.TotalPrice = totalPrice;
            booking.UserId = userId;
            booking.User = await _userRepository.GetByIdAsync(userId);

            await _bookingRepository.AddAsync(booking);

            return _mapper.Map<BookingResponse>(booking);
        }

        public async Task CancelBookingAsync(int bookingId)
        {
            if (!await _bookingRepository.ExistsAsync(bookingId)) throw new KeyNotFoundException("Booking not found");
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

            var room = await _roomRepository.GetByIdAsync(booking.RoomId);
            var hotel = await _hotelRepository.GetByIdAsync(booking.HotelId);
            var user = await _userRepository.GetByIdAsync(booking.UserId);

            var bookingResponse = _mapper.Map<BookingResponse>(booking);

            return bookingResponse;
        }

    }

}
