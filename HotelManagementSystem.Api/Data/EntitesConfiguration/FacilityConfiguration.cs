using HotelManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagementSystem.Api.Data.EntitesConfiguration;

public class FacilityConfiguration: IEntityTypeConfiguration<Facility>
{
    public void Configure(EntityTypeBuilder<Facility> builder)
    {
        builder.Property(e => e.Price).IsRequired().HasPrecision(18, 2);
       // builder.HasQueryFilter(e => !e.Deleted);
    }
}
