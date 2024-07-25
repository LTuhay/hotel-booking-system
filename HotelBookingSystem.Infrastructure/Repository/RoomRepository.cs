using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces.Repository;
using HotelBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task AddRangeAsync(IEnumerable<Room> rooms)
        {
            await _context.Set<Room>().AddRangeAsync(rooms);
            await _context.SaveChangesAsync();
        }
    }
}
