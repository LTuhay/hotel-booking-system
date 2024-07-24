using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
