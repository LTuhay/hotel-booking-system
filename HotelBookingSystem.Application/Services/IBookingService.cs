

using HotelBookingSystem.Application.DTO.BookingDTO;

namespace HotelBookingSystem.Application.Services
{
    public interface IBookingService
    {
        Task<BookingResponse> CreateBookingAsync(BookingRequest bookingRequest);
        Task CancelBookingAsync(int bookingId);
        Task<BookingResponse> UpdateBookingAsync(int bookingId, BookingRequest bookingRequest);
        Task<BookingResponse> GetBookingDetailsAsync(int bookingId);
    }
}
