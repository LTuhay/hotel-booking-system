using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces.Repository;
using HotelBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

}
