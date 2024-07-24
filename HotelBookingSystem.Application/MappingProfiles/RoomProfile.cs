
using AutoMapper;
using HotelBookingSystem.Application.DTO.RoomDTO;
using HotelBookingSystem.Domain.Entities;


namespace HotelBookingSystem.Application.MappingProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile() 
        {
            CreateMap<RoomRequest, Room>();
            CreateMap<Room, RoomResponse>();
        }
    }
}
