﻿using HotelBookingSystem.Domain.Enums;


namespace HotelBookingSystem.Domain.Entities
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Owner {  get; set; }
        public HotelType HotelType { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int StarRating { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<GuestReview> GuestReviews { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }

}
