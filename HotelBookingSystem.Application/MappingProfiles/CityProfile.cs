using AutoMapper;
using HotelBookingSystem.Application.DTO.CityDTO;
using HotelBookingSystem.Domain.Entities;


namespace HotelBookingSystem.Application.MappingProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile() 
        {
            CreateMap<CityRequest, City>();
            CreateMap<City, CityResponse>();
        }


    }
}
