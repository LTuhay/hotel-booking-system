using FluentValidation.AspNetCore;
using HotelBookingSystem.Application.MappingProfiles;
using HotelBookingSystem.Application.Validators;
using HotelBookingSystem.Infrastructure.Data;
using HotelBookingSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HotelBookingSystem.Application.JWT;
using HotelBookingSystem.Domain.Enums;
using HotelBookingSystem.Infrastructure.PasswordHasher;
using HotelBookingSystem.Domain.Interfaces.Repository;
using HotelBookingSystem.Application.Services;
using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Application.DTO.HotelDTO;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<BookingRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<CityRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<GuestReviewRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<HotelRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<PaymentRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<RoomRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UserRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<HotelSearchParametersValidator>();
    });

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole(UserRole.Admin.ToString()));
    options.AddPolicy("CustomerPolicy", policy => policy.RequireRole(UserRole.Customer.ToString()));
    options.AddPolicy("AdminOrCustomer", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserRole.Admin.ToString()) ||
            context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier));
    });
});


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<BookingProfile>();
    cfg.AddProfile<CityProfile>();
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
builder.Services.AddScoped<IGuestReviewRepository, GuestReviewRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<ISearchParameters, HotelSearchParameters>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddHttpContextAccessor();

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
