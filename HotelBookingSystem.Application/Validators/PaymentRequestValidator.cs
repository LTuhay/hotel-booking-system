using FluentValidation;
using HotelBookingSystem.Application.DTO.PaymentDTO;

namespace HotelBookingSystem.Application.Validators
{
    public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentRequestValidator()
        {
            RuleFor(x => x.BookingId).GreaterThan(0).WithMessage("BookingId must be greater than 0");
            RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("PaymentMethod is required");
        }
    }
}
