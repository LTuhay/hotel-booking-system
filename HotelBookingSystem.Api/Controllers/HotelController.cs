
using HotelBookingSystem.Application.DTO.GuestReviewDTO;
using HotelBookingSystem.Application.DTO.HotelDTO;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HotelBookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly IGuestReviewService _guestReviewService;

        public HotelController(IHotelService hotelService, IGuestReviewService guestReviewService)
        {
            _hotelService = hotelService;
            _guestReviewService = guestReviewService;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelRequest request)
        {
            try
            {
                var hotel = await _hotelService.CreateHotelAsync(request);
                return CreatedAtAction(nameof(GetHotelById), new { hotelId = hotel.HotelId }, hotel);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelById(int hotelId)
        {
            try
            {
                var hotel = await _hotelService.GetHotelByIdAsync(hotelId);
                return Ok(hotel);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }



        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{hotelId}")]
        public async Task<IActionResult> UpdateHotel(int hotelId, [FromBody] HotelRequest request)
        {
            try
            {
                var hotel = await _hotelService.UpdateHotelAsync(hotelId, request);
                return Ok(hotel);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{hotelId}")]
        public async Task<IActionResult> DeleteHotel(int hotelId)
        {
            try
            {
                await _hotelService.DeleteHotelAsync(hotelId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] HotelSearchParameters searchParameters)
        {
            try
            {
                var result = await _hotelService.SearchHotelsAsync(searchParameters);
                return Ok(result);
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("{hotelId}/reviews")]
        [Authorize(Policy = "AdminOrCustomer")]
        public async Task<IActionResult> AddGuestReview([FromBody] GuestReviewRequest reviewRequest)
        {
            try
            {
                var reviewResponse = await _guestReviewService.AddReviewAsync(reviewRequest);
                return Ok(reviewResponse);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }

        }

        [Authorize(Policy = "AdminOrCustomer")]
        [HttpDelete("{hotelId}/reviews/{reviewId}")]
        public async Task<IActionResult> DeleteGuestReview(int reviewId)
        {
            try
            {
                await _guestReviewService.DeleteReviewAsync(reviewId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }



        [HttpGet("featured-deals")]
        public async Task<IActionResult> GetFeaturedDeals([FromQuery] int? limit)
        {
            try
            {
                var effectiveLimit = limit.HasValue && limit.Value > 0 ? limit.Value : 5;
                var deals = await _hotelService.GetFeaturedDealsAsync(effectiveLimit);
                return Ok(deals);
        }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving special offers.");
    }
}

        [Authorize(Policy = "AdminOrCustomer")]
        [HttpGet("recent-hotels")]
        public async Task<IActionResult> GetRecentHotels([FromQuery] int limit = 5)
        {
            try
            {
                var recentHotels = await _hotelService.GetRecentHotelsAsync(limit);
                return Ok(recentHotels);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }
    }


}
