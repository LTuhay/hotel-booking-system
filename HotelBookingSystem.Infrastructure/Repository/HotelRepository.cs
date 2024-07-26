using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Enums;
using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Domain.Interfaces.Repository;
using HotelBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Repository
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<Hotel>> SearchAsync(ISearchParameters searchParameters)
        {
            var query = _context.Hotels
                .Include(h => h.Rooms)
                .ThenInclude(r => r.Bookings)
                .Include(h => h.GuestReviews)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchParameters.Query))
            {
                query = query.Where(h => h.Name.Contains(searchParameters.Query) ||
                                          h.City.Name.Contains(searchParameters.Query));
            }

            if (!string.IsNullOrEmpty(searchParameters.CheckInDate) && !string.IsNullOrEmpty(searchParameters.CheckOutDate))
            {
                if (DateTime.TryParse(searchParameters.CheckInDate, out DateTime checkInDate) &&
                    DateTime.TryParse(searchParameters.CheckOutDate, out DateTime checkOutDate))
                {
                    query = query.Where(h => h.Rooms.Any(r => r.Bookings.All(b => b.CheckOutDate <= checkInDate || b.CheckInDate >= checkOutDate)));
                }
            }

            if (searchParameters.Adults.HasValue)
            {
                query = query.Where(h => h.Rooms.Any(r => r.AdultCapacity >= searchParameters.Adults));
            }

            if (searchParameters.Children.HasValue)
            {
                query = query.Where(h => h.Rooms.Any(r => r.ChildCapacity >= searchParameters.Children));
            }

            if (searchParameters.MinPrice.HasValue)
            {
                query = query.Where(h => h.Rooms.Any(r => r.PricePerNight >= searchParameters.MinPrice));
            }

            if (searchParameters.MaxPrice.HasValue)
            {
                query = query.Where(h => h.Rooms.Any(r => r.PricePerNight <= searchParameters.MaxPrice));
            }

            if (searchParameters.MinRating.HasValue)
            {
                query = query.Where(h => h.StarRating >= searchParameters.MinRating);
            }

            if (searchParameters.MaxRating.HasValue)
            {
                query = query.Where(h => h.StarRating <= searchParameters.MaxRating);
            }

            if (searchParameters.Amenities != null && searchParameters.Amenities.Any())
            {
                var amenitiesList = searchParameters.Amenities
                    .SelectMany(a => a.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim()))
                    .ToList();

                var amenitiesEnums = amenitiesList
                    .Where(a => Enum.TryParse(typeof(HotelAmenity), a, out _))
                    .Select(a => (HotelAmenity)Enum.Parse(typeof(HotelAmenity), a))
                    .ToList();

                query = query.Where(h => amenitiesEnums.All(a => h.Amenities.Contains(a)));
            }

            if (searchParameters.RoomTypes != null && searchParameters.RoomTypes.Any())
            {
                var roomTypesList = searchParameters.RoomTypes
                    .SelectMany(rt => rt.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim()))
                    .ToList();

                var roomTypesEnums = roomTypesList
                    .Where(rt => Enum.TryParse(typeof(RoomType), rt, out _))
                    .Select(rt => (RoomType)Enum.Parse(typeof(RoomType), rt))
                    .ToList();

                query = query.Where(h => roomTypesEnums.All(rt => h.Rooms.Any(r => r.RoomType == rt)));
            }


            if (searchParameters.HotelType != null && searchParameters.HotelType.Any())
            {
                if (Enum.TryParse(searchParameters.HotelType, out HotelType hotelTypeEnum) && hotelTypeEnum != default)
                {
                    query = query.Where(h => h.HotelType == hotelTypeEnum);
                }
            }

            var totalResults = await query.CountAsync();

            var hotels = await query
                .Skip((searchParameters.Page - 1) * searchParameters.PageSize)
                .Take(searchParameters.PageSize)
                .ToListAsync();

            return hotels;
        }

        public override async Task<Hotel> GetByIdAsync(int id)
        {
            return await _context.Hotels
                .Include(h => h.Rooms) 
                .Include(h => h.GuestReviews)
                .FirstOrDefaultAsync(h => h.HotelId == id);
        }
    }

}
