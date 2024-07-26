using HotelBookingSystem.Application.DTO.BookingDTO;


namespace HotelBookingSystem.Application.Services
{
    public interface IPdfService
    {
        byte[] GenerateBookingPdf(BookingResponse bookingResponse);
    }
}
