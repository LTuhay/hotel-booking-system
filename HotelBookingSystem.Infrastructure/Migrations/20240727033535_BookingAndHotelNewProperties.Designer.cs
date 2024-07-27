﻿// <auto-generated />
using System;
using HotelBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelBookingSystem.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240727033535_BookingAndHotelNewProperties")]
    partial class BookingAndHotelNewProperties
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("HotelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpecialRequests")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BookingId");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PostOffice")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("Visitors")
                        .HasColumnType("INTEGER");

                    b.HasKey("CityId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.GuestReview", b =>
                {
                    b.Property<int>("GuestReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("HotelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GuestReviewId");

                    b.HasIndex("HotelId");

                    b.HasIndex("UserId");

                    b.ToTable("GuestReviews");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.Hotel", b =>
                {
                    b.Property<int>("HotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Amenities")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("HotelType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StarRating")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("HotelId");

                    b.HasIndex("CityId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<int>("BookingId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("PaymentId");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AdultCapacity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChildCapacity")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("DiscountedPrice")
                        .HasColumnType("TEXT");

                    b.Property<bool>("FeaturedDeal")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HotelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImagesUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PricePerNight")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("RoomId");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.Booking", b =>
                {
                    b.HasOne("HotelBookingSystem.Domain.Entities.Hotel", "Hotel")
                        .WithMany("Bookings")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HotelBookingSystem.Domain.Entities.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HotelBookingSystem.Domain.Entities.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.GuestReview", b =>
                {
                    b.HasOne("HotelBookingSystem.Domain.Entities.Hotel", "Hotel")
                        .WithMany("GuestReviews")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelBookingSystem.Domain.Entities.User", "User")
                        .WithMany("GuestReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.Hotel", b =>
                {
                    b.HasOne("HotelBookingSystem.Domain.Entities.City", "City")
                        .WithMany("Hotels")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.Payment", b =>
                {
                    b.HasOne("HotelBookingSystem.Domain.Entities.Booking", "Booking")
                        .WithOne("Payment")
                        .HasForeignKey("HotelBookingSystem.Domain.Entities.Payment", "BookingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.Room", b =>
                {
                    b.HasOne("HotelBookingSystem.Domain.Entities.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.Booking", b =>
                {
                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.City", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.Hotel", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("GuestReviews");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.Room", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("HotelBookingSystem.Domain.Entities.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("GuestReviews");
                });
#pragma warning restore 612, 618
        }
    }
}
