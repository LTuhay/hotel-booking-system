

namespace HotelBookingSystem.Domain.Entities
{
    public class FeaturedDeal
    {
        public int FeaturedDealId { get; set; }
        public int HotelId { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public Hotel Hotel { get; set; }
    }

}
