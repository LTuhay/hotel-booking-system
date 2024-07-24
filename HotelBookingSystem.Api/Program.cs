using FluentValidation.AspNetCore;
using HotelBookingSystem.Application.MappingProfiles;
using HotelBookingSystem.Application.Validators;
using HotelBookingSystem.Application.Services;
using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Infrastructure.Data;
using HotelBookingSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HotelBookingSystem.Application.JWT;
using HotelBookingSystem.Infrastructure;
using HotelBookingSystem.Domain.Enums;

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

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole(UserRole.Admin.ToString()));
    options.AddPolicy("CustomerPolicy", policy => policy.RequireRole(UserRole.Customer.ToString()));
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
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
