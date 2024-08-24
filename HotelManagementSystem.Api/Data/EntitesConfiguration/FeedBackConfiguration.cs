using HotelManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagementSystem.Api.Data.EntitesConfiguration;

public class FeedBackConfiguration :IEntityTypeConfiguration<FeedBack>
{
    public void Configure(EntityTypeBuilder<FeedBack> builder)
    {
        builder.HasIndex(e => new { e.CustomerId, e.ReservationId }).IsUnique();
    }
}
