using HotelBookingSystem.Application.DTO.PaymentDTO;
using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Application.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> CreatePaymentAsync(PaymentRequest paymentRequest);
        Task<PaymentResponse> GetPaymentDetailsAsync(int paymentId);
        Task<PaymentResponse> UpdatePaymentStatusAsync(int paymentId, string status);
    }
}
