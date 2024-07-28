

using HotelBookingSystem.Domain.Interfaces;

namespace HotelBookingSystem.Application.DTO.HotelDTO
{
    public class HotelSearchParameters : ISearchParameters
    {
        public string? Query { get; set; }
        public DateTime CheckInDate { get; set; } = DateTime.Today;
        public DateTime CheckOutDate { get; set; } = DateTime.Today.AddDays(1);
        public int? Adults { get; set; } = 2;
        public int? Children { get; set; } = 0;
        public int? Rooms { get; set; } = 1;
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinRating { get; set; }
        public int? MaxRating { get; set; }
        public IList<string>? Amenities { get; set; }
        public IList<string>? RoomTypes { get; set; }
        public string? HotelType { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
