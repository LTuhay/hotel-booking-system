using FluentValidation.TestHelper;
using HotelBookingSystem.Application.DTO.HotelDTO;
using HotelBookingSystem.Application.Validators;


namespace HotelBookingSystem.Tests.ValidatorTests
{
    public class HotelSearchParametersValidatorTests
    {
        private readonly HotelSearchParametersValidator _validator;

        public HotelSearchParametersValidatorTests()
        {
            _validator = new HotelSearchParametersValidator();
        }

        [Fact]
        public void ShouldHaveValidationErrorForQuery_WhenQueryContainsInvalidCharacters()
        {
            var model = new HotelSearchParameters
            {
                Query = "Invalid@Query" 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Query)
                .WithErrorMessage("Query contains invalid characters.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForCheckInDate_WhenDateIsNotInTheFuture()
        {
            var model = new HotelSearchParameters
            {
                CheckInDate = DateTime.Today.AddDays(-1),
                CheckOutDate = DateTime.Today
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.CheckInDate)
                .WithErrorMessage("Check-in date must be in the future.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForCheckOutDate_WhenDateIsBeforeCheckInDate()
        {
            var model = new HotelSearchParameters
            {
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(-1) 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.CheckOutDate)
                .WithErrorMessage("Check-out date must be later than check-in date.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForAdults_WhenNumberIsLessThanOne()
        {
            var model = new HotelSearchParameters
            {
                Adults = 0 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Adults)
                .WithErrorMessage("Number of adults must be at least 1.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForChildren_WhenNumberIsNegative()
        {
            var model = new HotelSearchParameters
            {
                Children = -1 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Children)
                .WithErrorMessage("Number of children cannot be negative.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForRooms_WhenNumberIsLessThanOne()
        {
            var model = new HotelSearchParameters
            {
                Rooms = 0 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Rooms)
                .WithErrorMessage("Number of rooms must be at least 1.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForMinPrice_WhenPriceIsNegative()
        {
            var model = new HotelSearchParameters
            {
                MinPrice = -1 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.MinPrice)
                .WithErrorMessage("Minimum price must be a non-negative number.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForMaxPrice_WhenPriceIsNegative()
        {
            var model = new HotelSearchParameters
            {
                MaxPrice = -1 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.MaxPrice)
                .WithErrorMessage("Maximum price must be a non-negative number.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForPriceRange_WhenMinPriceIsGreaterThanMaxPrice()
        {
            var model = new HotelSearchParameters
            {
                MinPrice = 150,
                MaxPrice = 100 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x)
                .WithErrorMessage("Minimum price should not be greater than maximum price.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForMinRating_WhenRatingIsOutOfRange()
        {
            var model = new HotelSearchParameters
            {
                MinRating = 0 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.MinRating)
                .WithErrorMessage("Minimum rating must be between 1 and 5.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForMaxRating_WhenRatingIsOutOfRange()
        {
            var model = new HotelSearchParameters
            {
                MaxRating = 6 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.MaxRating)
                .WithErrorMessage("Maximum rating must be between 1 and 5.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForRatingRange_WhenMinRatingIsGreaterThanMaxRating()
        {
            var model = new HotelSearchParameters
            {
                MinRating = 4,
                MaxRating = 3 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x)
                .WithErrorMessage("Minimum rating should not be greater than maximum rating.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForAmenities_WhenContainsInvalidStrings()
        {
            var model = new HotelSearchParameters
            {
                Amenities = new List<string> { "SwimmingPool", "", "Gym" } 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Amenities)
                .WithErrorMessage("All amenities must be valid non-empty strings.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForRoomTypes_WhenContainsInvalidStrings()
        {
            var model = new HotelSearchParameters
            {
                RoomTypes = new List<string> { "Suite", "", "Deluxe" } 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.RoomTypes)
                .WithErrorMessage("All room types must be valid non-empty strings.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForPage_WhenPageNumberIsNotPositive()
        {
            var model = new HotelSearchParameters
            {
                Page = 0 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Page)
                .WithErrorMessage("Page number must be a positive integer.");
        }

        [Fact]
        public void ShouldHaveValidationErrorForPageSize_WhenPageSizeIsOutOfRange()
        {
            var model = new HotelSearchParameters
            {
                PageSize = 51 
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.PageSize)
                .WithErrorMessage("Page size must be between 1 and 50.");
        }

        [Fact]
        public void ShouldNotHaveValidationErrors_WhenAllPropertiesAreValid()
        {
            var model = new HotelSearchParameters
            {
                Query = "ValidQuery",
                CheckInDate = DateTime.Today.AddDays(1), 
                CheckOutDate = DateTime.Today.AddDays(2),
                Adults = 1,
                Children = 0,
                Rooms = 1,
                MinPrice = 0,
                MaxPrice = 100,
                MinRating = 1,
                MaxRating = 5,
                Amenities = new List<string> { "SwimmingPool", "Gym" },
                RoomTypes = new List<string> { "Suite", "Deluxe" },
                Page = 1,
                PageSize = 10
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

