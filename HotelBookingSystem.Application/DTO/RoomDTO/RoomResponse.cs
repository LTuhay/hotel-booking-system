

using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Application.DTO.RoomDTO
{
    public class RoomResponse
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public bool FeaturedDeal { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int AdultCapacity { get; set; }
        public int ChildCapacity { get; set; }
        public ICollection<string> ImagesUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
