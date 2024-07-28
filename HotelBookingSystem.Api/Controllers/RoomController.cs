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
            var room = await _roomService.CreateRoomAsync(request);
            return CreatedAtAction(nameof(GetRoomById), new { roomId = room.RoomId }, room);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoomById(int roomId)
        {
            var room = await _roomService.GetRoomByIdAsync(roomId);
            return Ok(room);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{roomId}")]
        public async Task<IActionResult> UpdateRoom(int roomId, [FromBody] RoomRequest request)
        {
            var room = await _roomService.UpdateRoomAsync(roomId, request);
            return Ok(room);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{roomId}")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            await _roomService.DeleteRoomAsync(roomId);
            return NoContent();
        }
    }
}
