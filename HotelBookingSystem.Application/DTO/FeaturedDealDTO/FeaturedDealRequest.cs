

namespace HotelBookingSystem.Application.DTO.FeaturedDealDTO
{
    public class FeaturedDealRequest
    {
        public int HotelId { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}
