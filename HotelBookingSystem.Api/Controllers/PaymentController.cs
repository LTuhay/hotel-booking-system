using HotelBookingSystem.Application.DTO.PaymentDTO;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Api.Controllers
{
    namespace HotelBookingSystem.Api.Controllers
    {
        [ApiController]
        [Authorize(Policy = "AdminOrCustomer")]
        [Route("api/[controller]")]
        public class PaymentController : ControllerBase
        {
            private readonly IPaymentService _paymentService;

            public PaymentController(IPaymentService paymentService)
            {
                _paymentService = paymentService;
            }

            [HttpPost]
            public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest paymentRequest)
            {
                try
                {
                    var result = await _paymentService.CreatePaymentAsync(paymentRequest);
                    return CreatedAtAction(nameof(GetPaymentDetails), new { paymentId = result.PaymentId }, result);
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(new { message = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
                }
            }

            [HttpGet("{paymentId}")]
            public async Task<IActionResult> GetPaymentDetails(int paymentId)
            {
                try
                {
                    var result = await _paymentService.GetPaymentDetailsAsync(paymentId);
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

            [HttpPatch("{paymentId}/status")]
            public async Task<IActionResult> UpdatePaymentStatus(int paymentId, [FromBody] string status)
            {
                try
                {
                    var result = await _paymentService.UpdatePaymentStatusAsync(paymentId, status);
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
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
                }
            }
        }
    }
}