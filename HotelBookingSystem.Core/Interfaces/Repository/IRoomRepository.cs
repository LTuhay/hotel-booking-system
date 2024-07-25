using HotelBookingSystem.Domain.Entities;
using System.Threading.Tasks;

namespace HotelBookingSystem.Domain.Interfaces.Repository
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task AddRangeAsync(IEnumerable<Room> rooms);
    }
}
