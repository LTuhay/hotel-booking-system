

using AutoMapper;
using HotelBookingSystem.Application.DTO.FeaturedDealDTO;
using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Application.MappingProfiles
{
    public class FeaturedDealProfile : Profile
    { 
        public FeaturedDealProfile() 
        {
            CreateMap<FeaturedDealRequest, FeaturedDeal>();
            CreateMap<FeaturedDeal, FeaturedDealResponse>();
        }
    }
}
