
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using HotelBookingSystem.Domain.Entities;


namespace HotelBookingSystem.Application.DTO.BookingDTO
{
    public class BookingResponse
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
