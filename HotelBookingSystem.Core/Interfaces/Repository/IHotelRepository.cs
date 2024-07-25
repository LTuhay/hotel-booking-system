using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces.Repository
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<IList<Hotel>> SearchAsync(ISearchParameters searchParameters);
    }

}
