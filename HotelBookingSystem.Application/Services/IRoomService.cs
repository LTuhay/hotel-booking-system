using HotelBookingSystem.Application.DTO.HotelDTO;
using HotelBookingSystem.Application.DTO.RoomDTO;

namespace HotelBookingSystem.Application.Services
{
    public interface IRoomService
    {
        Task<RoomResponse> CreateRoomAsync(RoomRequest request);
        Task<RoomResponse> UpdateRoomAsync(int roomId, RoomRequest request);
        Task DeleteRoomAsync(int roomId);
        Task<RoomResponse> GetRoomByIdAsync(int roomId);
        Task<HotelResponse> AddRoomsToHotelAsync(int hotelId, List<RoomRequest> rooms);
    }
}
