using HotelManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagementSystem.Api.Data.EntitesConfiguration;

public class ReservationFacilityConfiguration : IEntityTypeConfiguration<ReservationRoomFacility>
{
    public void Configure(EntityTypeBuilder<ReservationRoomFacility> builder)
    {
        builder.HasIndex(e => new { e.ReservationId,e.RoomId, e.FacilityId}).IsUnique();
    }
}
