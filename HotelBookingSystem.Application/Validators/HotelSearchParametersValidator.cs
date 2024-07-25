using FluentValidation;
using HotelBookingSystem.Application.DTO.HotelDTO;

namespace HotelBookingSystem.Application.Validators
{
    public class HotelSearchParametersValidator : AbstractValidator<HotelSearchParameters>
    {
        public HotelSearchParametersValidator()
        {
            RuleFor(x => x.Query)
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Query contains invalid characters.");

            RuleFor(x => x.CheckInDate)
                .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("Check-in date must be in the format YYYY-MM-DD.")
                .When(x => !string.IsNullOrEmpty(x.CheckInDate));

            RuleFor(x => x.CheckOutDate)
                .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("Check-out date must be in the format YYYY-MM-DD.")
                .When(x => !string.IsNullOrEmpty(x.CheckOutDate));

            RuleFor(x => x)
                .Must(x => DateTime.Parse(x.CheckOutDate) > DateTime.Parse(x.CheckInDate))
                .WithMessage("Check-out date must be later than check-in date.")
                .When(x => !string.IsNullOrEmpty(x.CheckInDate) && !string.IsNullOrEmpty(x.CheckOutDate));

            RuleFor(x => x.Adults)
                .GreaterThan(0).WithMessage("Number of adults must be at least 1.");

            RuleFor(x => x.Children)
                .GreaterThanOrEqualTo(0).WithMessage("Number of children cannot be negative.");

            RuleFor(x => x.Rooms)
                .GreaterThan(0).WithMessage("Number of rooms must be at least 1.");

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum price must be a non-negative number.")
                .When(x => x.MinPrice.HasValue);

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Maximum price must be a non-negative number.")
                .When(x => x.MaxPrice.HasValue);

            RuleFor(x => x)
                .Must(x => x.MinPrice <= x.MaxPrice)
                .WithMessage("Minimum price should not be greater than maximum price.")
                .When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue);

            RuleFor(x => x.MinRating)
                .InclusiveBetween(1, 5).WithMessage("Minimum rating must be between 1 and 5.")
                .When(x => x.MinRating.HasValue);

            RuleFor(x => x.MaxRating)
                .InclusiveBetween(1, 5).WithMessage("Maximum rating must be between 1 and 5.")
                .When(x => x.MaxRating.HasValue);

            RuleFor(x => x)
                .Must(x => x.MinRating <= x.MaxRating)
                .WithMessage("Minimum rating should not be greater than maximum rating.")
                .When(x => x.MinRating.HasValue && x.MaxRating.HasValue);

            RuleFor(x => x.Amenities)
                .Must(x => x.All(a => !string.IsNullOrEmpty(a)))
                .WithMessage("All amenities must be valid non-empty strings.")
                .When(x => x.Amenities != null);

            RuleFor(x => x.RoomTypes)
                .Must(x => x.All(rt => !string.IsNullOrEmpty(rt)))
                .WithMessage("All room types must be valid non-empty strings.")
                .When(x => x.RoomTypes != null);

            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page number must be a positive integer.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50).WithMessage("Page size must be between 1 and 50.");
        }
    }

}
