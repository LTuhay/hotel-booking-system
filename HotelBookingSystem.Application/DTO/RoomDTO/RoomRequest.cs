

using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Application.DTO.RoomDTO
{
    public class RoomRequest
    {
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public int AdultCapacity { get; set; }
        public int ChildCapacity { get; set; }
        public bool FeaturedDeal { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public IList<string> ImagesUrl { get; set; }

    }
}
