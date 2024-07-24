

using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Application.DTO.GuestReviewDTO
{
    public class GuestReviewRequest
    {
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
