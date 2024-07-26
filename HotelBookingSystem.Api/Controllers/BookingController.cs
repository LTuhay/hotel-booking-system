
using HotelBookingSystem.Application.DTO.BookingDTO;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HotelBookingSystem.Api.Controllers
{
    [ApiController]
    [Authorize(Policy = "AdminOrCustomer")]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IPdfService _pdfService;

        public BookingController(IBookingService bookingService, IPdfService pdfService)
        {
            _bookingService = bookingService;
            _pdfService = pdfService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest bookingRequest)
        {
            try
            {
                var result = await _bookingService.CreateBookingAsync(bookingRequest);
                return CreatedAtAction(nameof(CreateBooking), result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
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

        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, [FromBody] BookingRequest bookingRequest)
        {
            try
            {
                var result = await _bookingService.UpdateBookingAsync(bookingId, bookingRequest);
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

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingDetails(int bookingId)
        {
            try
            {
                var result = await _bookingService.GetBookingDetailsAsync(bookingId);
                return Ok(result);
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

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            try
            {
                await _bookingService.CancelBookingAsync(bookingId);
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

        [HttpGet("{bookingId}/pdf")]
        public async Task<IActionResult> GetBookingPdf(int bookingId)
        {
            try
            {
                var bookingResponse = await _bookingService.GetBookingDetailsAsync(bookingId);
                if (bookingResponse == null)
                {
                    return NotFound(new { message = "Booking not found" });
                }

                var pdfBytes = _pdfService.GenerateBookingPdf(bookingResponse);

                return File(pdfBytes, "application/pdf", $"Booking_{bookingId}.pdf");
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

