using HotelBookingSystem.Application.DTO.RoomDTO;
using HotelBookingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomRequest request)
        {
            try
            {
                var room = await _roomService.CreateRoomAsync(request);
                return CreatedAtAction(nameof(GetRoomById), new { roomId = room.RoomId }, room);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoomById(int roomId)
        {
            var room = await _roomService.GetRoomByIdAsync(roomId);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{roomId}")]
        public async Task<IActionResult> UpdateRoom(int roomId, [FromBody] RoomRequest request)
        {
            try
            {
                var room = await _roomService.UpdateRoomAsync(roomId, request);
                return Ok(room);
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
        [HttpDelete("{roomId}")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            try
            {
                await _roomService.DeleteRoomAsync(roomId);
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
    }
}
