

using HotelBookingSystem.Application.DTO.GuestReviewDTO;

namespace HotelBookingSystem.Application.Services
{
    public interface IGuestReviewService
    {
        Task<GuestReviewResponse> AddReviewAsync(GuestReviewRequest reviewRequest);
        Task DeleteReviewAsync(int reviewId);

    }
}
