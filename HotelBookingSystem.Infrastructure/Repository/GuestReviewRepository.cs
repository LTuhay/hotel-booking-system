using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Repository
{
    public class GuestReviewRepository : Repository<GuestReview>, IGuestReviewRepository
    {
        public GuestReviewRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

}
