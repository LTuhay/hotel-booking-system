using AutoMapper;
using HotelBookingSystem.Application.DTO.HotelDTO;
using HotelBookingSystem.Application.DTO.RoomDTO;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces.Repository;

namespace HotelBookingSystem.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IHotelRepository hotelRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<RoomResponse> CreateRoomAsync(RoomRequest request)
        {
            var room = _mapper.Map<Room>(request);
            if (!await _hotelRepository.ExistsAsync(room.HotelId))
            {
                throw new KeyNotFoundException("Hotel not found");
            }
            room.CreatedAt = DateTime.UtcNow;
            room.UpdatedAt = DateTime.UtcNow;

            var createdRoom = await _roomRepository.AddAsync(room);
            return _mapper.Map<RoomResponse>(createdRoom);
        }

        public async Task<RoomResponse> UpdateRoomAsync(int roomId, RoomRequest request)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);
            if (room == null)
                throw new KeyNotFoundException("Room not found");

            _mapper.Map(request, room);
            room.UpdatedAt = DateTime.UtcNow;

            var updatedRoom = await _roomRepository.UpdateAsync(room);
            return _mapper.Map<RoomResponse>(updatedRoom);
        }

        public async Task DeleteRoomAsync(int roomId)
        {

            await _roomRepository.DeleteAsync(roomId);
        }

        public async Task<RoomResponse> GetRoomByIdAsync(int roomId)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);
            if (room == null)
                throw new KeyNotFoundException("Room not found");
            return _mapper.Map<RoomResponse>(room);
        }

        public async Task<HotelResponse> AddRoomsToHotelAsync(int hotelId, List<RoomRequest> rooms)
        {
            var hotel = await _hotelRepository.GetByIdAsync(hotelId);
            if (hotel == null)
                throw new KeyNotFoundException("Hotel not found");

            var roomEntities = rooms.Select(r => _mapper.Map<Room>(r)).ToList();
            roomEntities.ForEach(r => r.CreatedAt = DateTime.UtcNow);
            roomEntities.ForEach(r => r.UpdatedAt = DateTime.UtcNow);

            await _roomRepository.AddRangeAsync(roomEntities);

            return _mapper.Map<HotelResponse>(hotel);
        }
    }
}
