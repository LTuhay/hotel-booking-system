using FluentValidation;
using HotelBookingSystem.Application.DTO.PaymentDTO;

namespace HotelBookingSystem.Application.Validators
{
    public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentRequestValidator()
        {
            RuleFor(x => x.BookingId).GreaterThan(0).WithMessage("BookingId must be greater than 0");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
            RuleFor(x => x.PaymentDate).NotEmpty().WithMessage("PaymentDate is required");
            RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("PaymentMethod is required");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
        }
    }
}
