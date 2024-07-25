using HotelBookingSystem.Application.DTO.HotelDTO;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace HotelBookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelById(int hotelId)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(hotelId);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
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
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
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
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] HotelSearchParameters searchParameters)
        {
            var result = await _hotelService.SearchHotelsAsync(searchParameters);
            return Ok(result);
        }
    }
}
