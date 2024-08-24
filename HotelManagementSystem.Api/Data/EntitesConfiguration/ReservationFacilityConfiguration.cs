using HotelManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagementSystem.Api.Data.EntitesConfiguration;

public class ReservationFacilityConfiguration : IEntityTypeConfiguration<ReservationFacility>
{
    public void Configure(EntityTypeBuilder<ReservationFacility> builder)
    {
        builder.HasIndex(e => new { e.ReservationId, e.FacilityId }).IsUnique();
    }
}
