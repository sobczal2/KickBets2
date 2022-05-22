using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Persistence.Configuration.Entities;

public class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
{
    public void Configure(EntityTypeBuilder<Fixture> builder)
    {
        builder
            .Property(f => f.Id)
            .ValueGeneratedNever();

        builder
            .HasOne(f => f.HomeTeam)
            .WithMany()
            .HasForeignKey(f => f.HomeTeamId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .HasOne(f => f.AwayTeam)
            .WithMany()
            .HasForeignKey(f => f.AwayTeamId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .HasOne(f => f.Status)
            .WithOne(s => s.Fixture)
            .HasForeignKey<Fixture>(f => f.StatusId);

        builder
            .HasOne(f => f.Score)
            .WithOne(s => s.Fixture)
            .HasForeignKey<Fixture>(f => f.ScoreId);

        builder
            .HasOne(f => f.HomeLineup)
            .WithOne(l => l.Fixture)
            .HasForeignKey<Fixture>(f => f.HomeLineupId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .HasOne(f => f.AwayLineup)
            .WithOne(l => l.Fixture)
            .HasForeignKey<Fixture>(f => f.AwayLineupId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(f => f.BetsData)
            .WithOne(bd => bd.Fixture)
            .HasForeignKey<Fixture>(f => f.BetsDataId);
    }
}