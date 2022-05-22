using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Persistence.Configuration.Entities;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder
            .Property(v => v.Id)
            .ValueGeneratedNever();
    }
}