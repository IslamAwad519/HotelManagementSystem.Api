using System.Diagnostics;
using System.Text;
using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.MappingProfiles;
using HotelManagementSystem.Api.Models;
using HotelManagementSystem.Api.Repository;
using HotelManagementSystem.Api.Roles;
using HotelManagementSystem.Api.Services.Authentication;
using HotelManagementSystem.Api.Services.Facilities;
using HotelManagementSystem.Api.Services.Offers;
using HotelManagementSystem.Api.Services.Payment;
using HotelManagementSystem.Api.Services.Reservations;
using HotelManagementSystem.Api.Services.Rooms;
using HotelManagementSystem.Api.Services.RoomTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer abcdefghijklmnopqrstuvwxyz\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,

                },
                new List<string>()
            }
        });

    });


    builder.Services.AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("sqlconnection"))
        .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
        .EnableSensitiveDataLogging();
    });

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
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
    builder.Services.AddScoped<IJwtService, JwtService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
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

    app.UseAuthentication();
    app.UseAuthorization();

    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using var scope = scopeFactory.CreateScope();
    var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DefaultRoles.SeedAsync(roleManger);

    app.UseCors();
    app.MapControllers();

    app.Run();
}
