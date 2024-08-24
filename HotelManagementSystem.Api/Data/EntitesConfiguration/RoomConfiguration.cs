using HotelManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagementSystem.Api.Data.EntitesConfiguration;

public class RoomConfiguration :IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasIndex(e => e.RoomNumber).IsUnique();
        builder.Property(e => e.RoomNumber).HasMaxLength(10);
       // builder.HasQueryFilter(e => !e.Deleted);
    }
}
