using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces.Repository
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<IList<Hotel>> SearchAsync(ISearchParameters searchParameters);
        Task<IEnumerable<Hotel>> GetFeaturedDealsAsync(int limit);
        Task<IEnumerable<Hotel>> GetRecientHotelsAsync(int userId, int limit);
    }

}
