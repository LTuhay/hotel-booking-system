using FluentValidation.AspNetCore;
using HotelBookingSystem.Application.MappingProfiles;
using HotelBookingSystem.Application.Validators;
using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Infrastructure.Data;
using HotelBookingSystem.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<BookingRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<CityRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<FeaturedDealRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<GuestReviewRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<HotelRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<PaymentRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<RoomRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UserRequestValidator>();
    });

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<BookingProfile>();
    cfg.AddProfile<CityProfile>();
    cfg.AddProfile<FeaturedDealProfile>();
    cfg.AddProfile<GuestReviewProfile>();
    cfg.AddProfile<HotelProfile>();
    cfg.AddProfile<PaymentProfile>();
    cfg.AddProfile<RoomProfile>();
    cfg.AddProfile<UserProfile>();
}, typeof(Program).Assembly);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IFeaturedDealRepository, FeaturedDealRepository>();
builder.Services.AddScoped<IGuestReviewRepository, GuestReviewRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
