using AutoMapper;
using FluentAssertions;
using HotelBookingSystem.Application.DTO.HotelDTO;
using HotelBookingSystem.Application.DTO.RoomDTO;
using HotelBookingSystem.Application.MappingProfiles;
using HotelBookingSystem.Application.Services;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces.Repository;
using Moq;

namespace HotelBookingSystem.Tests.ServiceTests
{
    public class RoomServiceTests
    {
        private readonly RoomService _roomService;
        private readonly Mock<IRoomRepository> _roomRepositoryMock;
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly IMapper _mapper;

        public RoomServiceTests()
        {
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _hotelRepositoryMock = new Mock<IHotelRepository>();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RoomProfile>();
                cfg.AddProfile<HotelProfile>();
            });
            _mapper = configuration.CreateMapper();

            _roomService = new RoomService(_roomRepositoryMock.Object, _hotelRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task CreateRoomAsync_ShouldReturnRoomResponse()
        {
            var request = new RoomRequest { HotelId = 1 };
            var room = new Room { RoomId = 1, HotelId = 1 };
            var roomResponse = _mapper.Map<RoomResponse>(room);

            _hotelRepositoryMock.Setup(repo => repo.ExistsAsync(request.HotelId)).ReturnsAsync(true);
            _roomRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Room>())).ReturnsAsync(room);

            var result = await _roomService.CreateRoomAsync(request);

            result.Should().BeEquivalentTo(roomResponse);
        }

        [Fact]
        public async Task UpdateRoomAsync_ShouldReturnUpdatedRoomResponse()
        {
            var roomId = 1;
            var request = new RoomRequest { HotelId = 1 };
            var room = new Room { RoomId = roomId, HotelId = 1 };
            var updatedRoom = new Room { RoomId = roomId, HotelId = 1 };
            var roomResponse = _mapper.Map<RoomResponse>(updatedRoom);

            _roomRepositoryMock.Setup(repo => repo.GetByIdAsync(roomId)).ReturnsAsync(room);
            _mapper.Map(request, room);
            _roomRepositoryMock.Setup(repo => repo.UpdateAsync(room)).ReturnsAsync(updatedRoom);

            var result = await _roomService.UpdateRoomAsync(roomId, request);

            result.Should().BeEquivalentTo(roomResponse);
        }

        [Fact]
        public async Task DeleteRoomAsync_ShouldCallDeleteAsync()
        {
            var roomId = 1;

            await _roomService.DeleteRoomAsync(roomId);

            _roomRepositoryMock.Verify(repo => repo.DeleteAsync(roomId), Times.Once);
        }

        [Fact]
        public async Task GetRoomByIdAsync_ShouldReturnRoomResponse()
        {
            var roomId = 1;
            var room = new Room { RoomId = roomId };
            var roomResponse = _mapper.Map<RoomResponse>(room);

            _roomRepositoryMock.Setup(repo => repo.GetByIdAsync(roomId)).ReturnsAsync(room);

            var result = await _roomService.GetRoomByIdAsync(roomId);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(roomResponse);
        }

        [Fact]
        public async Task AddRoomsToHotelAsync_ShouldReturnHotelResponse()
        {
            var hotelId = 1;
            var rooms = new List<RoomRequest> { new RoomRequest { HotelId = hotelId } };
            var hotel = new Hotel { HotelId = hotelId };
            var hotelResponse = _mapper.Map<HotelResponse>(hotel);
            var roomEntities = new List<Room> { new Room { HotelId = hotelId } };

            _hotelRepositoryMock.Setup(repo => repo.GetByIdAsync(hotelId)).ReturnsAsync(hotel);
            _roomRepositoryMock.Setup(repo => repo.AddRangeAsync(roomEntities));

            var result = await _roomService.AddRoomsToHotelAsync(hotelId, rooms);

            result.Should().BeEquivalentTo(hotelResponse);
        }
    }
}
