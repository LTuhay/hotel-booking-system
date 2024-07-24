using FluentValidation;
using HotelBookingSystem.Application.DTO.RoomDTO;

namespace HotelBookingSystem.Application.Validators
{
    public class RoomRequestValidator : AbstractValidator<RoomRequest>
    {
        public RoomRequestValidator()
        {
            RuleFor(x => x.HotelId).GreaterThan(0).WithMessage("HotelId must be greater than 0");
            RuleFor(x => x.RoomType).NotEmpty().WithMessage("RoomType is required");
            RuleFor(x => x.PricePerNight).GreaterThan(0).WithMessage("PricePerNight must be greater than 0");
            RuleFor(x => x.AdultCapacity).GreaterThan(0).WithMessage("AdultCapacity must be greater than 0");
            RuleFor(x => x.ChildCapacity).GreaterThanOrEqualTo(0).WithMessage("ChildCapacity must be greater than or equal to 0");
            RuleFor(x => x.ImagesUrl).NotNull().WithMessage("ImagesUrl cannot be null");
        }
    }
}
