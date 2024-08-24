using HotelManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagementSystem.Api.Data.EntitesConfiguration;

public class RoomFacilityConfiguration : IEntityTypeConfiguration<RoomFacility>
{
    public void Configure(EntityTypeBuilder<RoomFacility> builder)
    {
        builder.HasIndex(e => new { e.RoomId, e.FacilityId }).IsUnique();
    }
}
