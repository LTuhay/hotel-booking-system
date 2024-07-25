using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces.Repository;
using HotelBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

}
