using System.Diagnostics;
using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.MappingProfiles;
using HotelManagementSystem.Api.Repository;
using HotelManagementSystem.Api.Services.Facilities;
using HotelManagementSystem.Api.Services.Offers;
using HotelManagementSystem.Api.Services.Rooms;
using HotelManagementSystem.Api.Services.RoomTypes;
using Microsoft.EntityFrameworkCore;

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
    builder.Services.AddScoped<IRoomService, RoomService>();
    builder.Services.AddScoped<IFacilityService, FacilityService>();
    builder.Services.AddScoped<IOfferService, OfferService>();
    builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
}

var app = builder.Build();
{

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
