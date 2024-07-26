using FluentValidation;
using HotelBookingSystem.Application.DTO.GuestReviewDTO;

namespace HotelBookingSystem.Application.Validators
{
    public class GuestReviewRequestValidator : AbstractValidator<GuestReviewRequest>
    {
        public GuestReviewRequestValidator()
        {
            RuleFor(x => x.HotelId).GreaterThan(0).WithMessage("HotelId must be greater than 0");
            RuleFor(x => x.Rating).InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Comment is required");
        }
    }
}
