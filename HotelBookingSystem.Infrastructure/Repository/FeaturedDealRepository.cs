using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Repository
{
    public class FeaturedDealRepository : Repository<FeaturedDeal>, IFeaturedDealRepository
    {
        public FeaturedDealRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

}
