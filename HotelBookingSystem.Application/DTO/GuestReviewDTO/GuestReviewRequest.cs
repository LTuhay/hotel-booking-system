

namespace HotelBookingSystem.Application.DTO.GuestReviewDTO
{
    public class GuestReviewRequest
    {
        public int HotelId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
