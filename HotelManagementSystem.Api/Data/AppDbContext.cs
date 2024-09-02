using System.Reflection;
using HotelManagementSystem.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Data;

public class AppDbContext :IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomFacility> RoomFacilities { get; set; }
    public DbSet<Facility> Facilities { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<ReservationRoomFacility> ReservationRoomFacilities { get; set; }
    public DbSet<ReservationRoom> ReservationRooms { get; set; }
    public DbSet<FeedBack> FeedBacks { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        //Restrict 
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        base.OnModelCreating(modelBuilder);
    }
}
