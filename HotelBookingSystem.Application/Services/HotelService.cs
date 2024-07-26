using AutoMapper;
using HotelBookingSystem.Application.DTO.HotelDTO;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Domain.Interfaces.Repository;
using HotelBookingSystem.Domain.Utilities;

namespace HotelBookingSystem.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper, ICityRepository cityRepository)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        public async Task<HotelResponse> CreateHotelAsync(HotelRequest request)
        {
            var hotel = _mapper.Map<Hotel>(request);
            if (!await _cityRepository.ExistsAsync(hotel.CityId))
            {
                throw new KeyNotFoundException("City not found");
            }
            hotel.CreatedAt = DateTime.UtcNow;
            hotel.UpdatedAt = DateTime.UtcNow;

            var createdHotel = await _hotelRepository.AddAsync(hotel);
            return _mapper.Map<HotelResponse>(createdHotel);
        }

        public async Task<HotelResponse> UpdateHotelAsync(int hotelId, HotelRequest request)
        {
            var hotel = await _hotelRepository.GetByIdAsync(hotelId);
            if (hotel == null)
                throw new KeyNotFoundException("Hotel not found");

            _mapper.Map(request, hotel);
            hotel.UpdatedAt = DateTime.UtcNow;

            var updatedHotel = await _hotelRepository.UpdateAsync(hotel);
            return _mapper.Map<HotelResponse>(updatedHotel);
        }

        public async Task DeleteHotelAsync(int hotelId)
        {
            await _hotelRepository.DeleteAsync(hotelId);
        }

        public async Task<HotelResponse> GetHotelByIdAsync(int hotelId)
        {
            var hotel = await _hotelRepository.GetByIdAsync(hotelId);
            if (hotel == null)
                return null;

            return _mapper.Map<HotelResponse>(hotel);
        }

        public async Task<IPaginatedList<HotelResponse>> SearchHotelsAsync(HotelSearchParameters searchParameters)
        {
            var hotels = await _hotelRepository.SearchAsync(searchParameters);

            var totalResults = hotels.Count;
            var totalPages = (int)Math.Ceiling(totalResults / (double)searchParameters.PageSize);
            var hotelResponses = _mapper.Map<List<HotelResponse>>(hotels);

            CheckRoomsAvailability(searchParameters, hotels, hotelResponses);

            var paginatedList = new PaginatedList<HotelResponse>
            {
                TotalResults = totalResults,
                CurrentPage = searchParameters.Page,
                PageSize = searchParameters.PageSize,
                TotalPages = totalPages,
                Items = hotelResponses
            };

            return paginatedList;
        }

        private static void CheckRoomsAvailability(HotelSearchParameters searchParameters, IList<Hotel> hotels, List<HotelResponse> hotelResponses)
        {
            DateTime? checkInDate = null;
            DateTime? checkOutDate = null;
            if (!string.IsNullOrEmpty(searchParameters.CheckInDate) && !string.IsNullOrEmpty(searchParameters.CheckOutDate))
            {
                if (DateTime.TryParse(searchParameters.CheckInDate, out DateTime parsedCheckInDate) &&
                    DateTime.TryParse(searchParameters.CheckOutDate, out DateTime parsedCheckOutDate))
                {
                    checkInDate = parsedCheckInDate;
                    checkOutDate = parsedCheckOutDate;
                }
            }

            foreach (var hotelResponse in hotelResponses)
            {
                var hotel = hotels.First(h => h.HotelId == hotelResponse.HotelId);
                foreach (var roomResponse in hotelResponse.Rooms)
                {
                    var room = hotel.Rooms.First(r => r.RoomId == roomResponse.RoomId);
                    if (checkInDate.HasValue && checkOutDate.HasValue)
                    {
                        roomResponse.IsAvailable = room.Bookings
                            .All(b => b.CheckOutDate <= checkInDate || b.CheckInDate >= checkOutDate);
                    }
                    else
                    {
                        roomResponse.IsAvailable = room.Bookings
                            .All(b => b.CheckOutDate <= DateTime.Now || b.CheckInDate >= DateTime.Now);
                    }
                }
            }
        }
    }
}
