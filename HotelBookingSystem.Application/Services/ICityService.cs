using HotelBookingSystem.Application.DTO.CityDTO;

namespace HotelBookingSystem.Application.Services
{
    public interface ICityService
    {
        Task<CityResponse> CreateCityAsync(CityRequest request);
        Task<CityResponse> GetCityByIdAsync(int id);
        Task<CityResponse> UpdateCityAsync(int id, CityRequest request);
        Task DeleteCityAsync(int id);
    }
}
