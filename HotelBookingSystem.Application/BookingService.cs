
using AutoMapper.Collection;
using AutoMapper;
using HotelBookingSystem.Application.DTO.BookingDTO;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Repository;
using HotelBookingSystem.Domain.Interfaces.Repository;

namespace HotelBookingSystem.Application
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper mapper;

        public BookingService (IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            this.mapper = mapper;
        }
        public void AddBooking (BookingResponse booking)
        {
            var bookingEntity = mapper.Map<Booking>(booking);
            
        }
    }
}
