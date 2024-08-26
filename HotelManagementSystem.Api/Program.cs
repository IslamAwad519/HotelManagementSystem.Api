using System.Diagnostics;
using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.MappingProfiles;
using HotelManagementSystem.Api.Payment;
using HotelManagementSystem.Api.Repository;
using HotelManagementSystem.Api.Services.Facilities;
using HotelManagementSystem.Api.Services.Offers;
using HotelManagementSystem.Api.Services.Payment;
using HotelManagementSystem.Api.Services.Reservations;
using HotelManagementSystem.Api.Services.Rooms;
using HotelManagementSystem.Api.Services.RoomTypes;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("sqlconnection"))
        .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
        .EnableSensitiveDataLogging();
    });
    builder.Services.AddAutoMapper(typeof(RoomProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(FacilityProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(RoomTypeProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(OfferProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(FeedBackProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(ImageProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(RoomFacilityProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(ReservationProfile).Assembly);
    //builder.Services.AddAutoMapper(typeof(ReservationFacilityProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(ReservationRoomProfile).Assembly);

    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    builder.Services.AddScoped<IRoomService, RoomService>();
    builder.Services.AddScoped<IFacilityService, FacilityService>();
    builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
    builder.Services.AddScoped<IReservationService, ReservationService>();
    builder.Services.AddScoped<IOfferService, OfferService>();

    builder.Services.AddScoped<PaymentService>();
    builder.Services.AddCors(opt =>
    {
        opt.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
    });
    StripeConfiguration.ApiKey = builder.Configuration.GetSection("StripeSetting:SecretKey").Get<string>();
    //builder.Services.Configure<StripeSetting>(builder.Configuration.GetSection("StripeSetting"));   
}

var app = builder.Build();
{

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
 

    var test = builder.Configuration.GetSection("StripeSetting:SecretKey").Get<string>();
    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseCors();
    app.MapControllers();

    app.Run();
}
