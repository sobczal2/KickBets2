using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sobczal1.KickBets.Domain;
using Sobczal1.KickBets.Domain.Bets;
using Sobczal1.KickBets.Domain.Football;
using Sobczal1.KickBets.Domain.Identity;

namespace Sobczal1.KickBets.Persistence;

public class KickBetsDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
    public KickBetsDbContext()
    {
    }

    public KickBetsDbContext(DbContextOptions<KickBetsDbContext> options) : base(options)
    {
    }

    public DbSet<BetsData> BetsData { get; set; }
    public DbSet<BaseBet> Bets { get; set; }
    public DbSet<WdlhtBet> WdlhtBets { get; set; }
    public DbSet<WdlftBet> WdlftBets { get; set; }

    public DbSet<Event> Events { get; set; }
    public DbSet<HomeEvent> HomeEvents { get; set; }
    public DbSet<AwayEvent> AwayEvents { get; set; }
    public DbSet<Fixture> Fixtures { get; set; }
    public DbSet<League> Leagues { get; set; }
    public DbSet<Lineup> Lineups { get; set; }
    public DbSet<HomeLineup> HomeLineups { get; set; }
    public DbSet<AwayLineup> AwayLineups { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<HomeStatistics> HomeStatistics { get; set; }
    public DbSet<AwayStatistics> AwayStatistics { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<DbUpdate> DbUpdates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KickBetsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}