using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Persistence.Configuration.Entities;

public class LeagueConfiguration : IEntityTypeConfiguration<League>
{
    public void Configure(EntityTypeBuilder<League> builder)
    {
        builder
            .Property(l => l.Id)
            .ValueGeneratedNever();
    }
}