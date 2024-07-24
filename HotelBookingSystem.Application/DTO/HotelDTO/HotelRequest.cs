

using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Application.DTO.HotelDTO
{
    public class HotelRequest
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string HotelType { get; set; }
        public int CityId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }

    }
}
