using HotelBookingSystem.Application.DTO.BookingDTO;


namespace HotelBookingSystem.Infrastructure.PdfGenerator
{
    public interface IPdfService
    {
        byte[] GenerateBookingPdf(BookingResponse bookingResponse);
    }
}
