
using AutoMapper;
using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Application.DTO.BookingDTO
{
    public class BookingRequest
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
