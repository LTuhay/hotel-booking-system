using HotelBookingSystem.Application.DTO.BookingDTO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace HotelBookingSystem.Application.Services
{
    public class PdfService : IPdfService
    {
        private readonly IBookingService _bookingService;
        public PdfService(IBookingService bookingService) 
        {
            _bookingService = bookingService;
        }
        public byte[] GenerateBookingPdf(BookingResponse bookingResponse)
        {

            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                document.Add(new Paragraph("Booking Details")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20));

                document.Add(new Paragraph($"Booking ID: {bookingResponse.BookingId}"));
                document.Add(new Paragraph($"User Name: {bookingResponse.UserFirstName} {bookingResponse.UserLastName}"));
                document.Add(new Paragraph($"Hotel Name: {bookingResponse.HotelName}"));
                document.Add(new Paragraph($"Room Type: {bookingResponse.RoomType}"));
                document.Add(new Paragraph($"Check-in Date: {bookingResponse.CheckInDate.ToString("d")}"));
                document.Add(new Paragraph($"Check-out Date: {bookingResponse.CheckOutDate.ToString("d")}"));
                document.Add(new Paragraph($"Total Price: ${bookingResponse.TotalPrice}"));

                if (bookingResponse.Payment != null)
                {
                    document.Add(new Paragraph("Payment Details"));
                    document.Add(new Paragraph($"Payment ID: {bookingResponse.Payment.PaymentId}"));
                    document.Add(new Paragraph($"Amount: ${bookingResponse.Payment.Amount}"));
                    document.Add(new Paragraph($"Payment Date: {bookingResponse.Payment.PaymentDate.ToString("d")}"));
                    document.Add(new Paragraph($"Payment Method: {bookingResponse.Payment.PaymentMethod}"));
                    document.Add(new Paragraph($"Payment Status: {bookingResponse.Payment.Status}"));
                }

                document.Close();

                return ms.ToArray();
            }
         }
    }
}