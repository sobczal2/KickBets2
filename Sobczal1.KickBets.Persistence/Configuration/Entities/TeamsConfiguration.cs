using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Persistence.Configuration.Entities;

public class TeamsConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder
            .Property(t => t.Id)
            .ValueGeneratedNever();
    }
}