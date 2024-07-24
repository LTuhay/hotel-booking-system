using FluentValidation;
using HotelBookingSystem.Application.DTO.HotelDTO;

namespace HotelBookingSystem.Application.Validators
{
    public class HotelRequestValidator : AbstractValidator<HotelRequest>
    {
        public HotelRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Owner).NotEmpty().WithMessage("Owner is required");
            RuleFor(x => x.HotelType).NotEmpty().WithMessage("HotelType is required");
            RuleFor(x => x.CityId).GreaterThan(0).WithMessage("CityId must be greater than 0");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ThumbnailUrl).NotEmpty().WithMessage("ThumbnailUrl is required");
        }
    }
}
