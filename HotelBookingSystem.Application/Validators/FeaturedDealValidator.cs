using FluentValidation;
using HotelBookingSystem.Application.DTO.FeaturedDealDTO;

namespace HotelBookingSystem.Application.Validators
{
    public class FeaturedDealRequestValidator : AbstractValidator<FeaturedDealRequest>
    {
        public FeaturedDealRequestValidator()
        {
            RuleFor(x => x.HotelId).GreaterThan(0).WithMessage("HotelId must be greater than 0");
            RuleFor(x => x.OriginalPrice).GreaterThan(0).WithMessage("OriginalPrice must be greater than 0");
            RuleFor(x => x.DiscountedPrice).GreaterThan(0).WithMessage("DiscountedPrice must be greater than 0");
        }
    }
}
