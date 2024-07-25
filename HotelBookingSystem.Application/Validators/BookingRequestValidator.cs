using FluentValidation;
using HotelBookingSystem.Application.DTO.BookingDTO;

namespace HotelBookingSystem.Application.Validators
{
    public class BookingRequestValidator : AbstractValidator<BookingRequest>
    {
        public BookingRequestValidator()
        {
            RuleFor(x => x.RoomId).GreaterThan(0).WithMessage("RoomId must be greater than 0");
            RuleFor(x => x.HotelId).GreaterThan(0).WithMessage("HotelId must be greater than 0");
            RuleFor(x => x.CheckInDate).GreaterThan(DateTime.Now).WithMessage("CheckInDate must be in the future");
            RuleFor(x => x.CheckOutDate).GreaterThan(x => x.CheckInDate).WithMessage("CheckOutDate must be after CheckInDate");
        }
    }
}
