

namespace HotelBookingSystem.Application.DTO.HotelDTO
{
    public class HotelResponse
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string HotelType { get; set; }
        public int CityId { get; set; }
        public int StarRating { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
