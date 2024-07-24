using HotelBookingSystem.Domain.Entities;


namespace HotelBookingSystem.Application.DTO.PaymentDTO
{
    public class PaymentRequest
    {
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}
