using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sobczal1.KickBets.Domain;
using Sobczal1.KickBets.Domain.Bets;
using Sobczal1.KickBets.Domain.Bets.Wdlft;
using Sobczal1.KickBets.Domain.Bets.Wdlht;
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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KickBetsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<BaseBet> Bets { get; set; } = null!;
    public DbSet<WdlftBet> WdlftBets { get; set; } = null!;
    public DbSet<WdlhtBet> WdlhtBets { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<HomeEvent> HomeEvents { get; set; } = null!;
    public DbSet<AwayEvent> AwayEvents { get; set; } = null!;
    public DbSet<Fixture> Fixtures { get; set; } = null!;
    public DbSet<League> Leagues { get; set; } = null!;
    public DbSet<Lineup> Lineups { get; set; } = null!;
    public DbSet<HomeLineup> HomeLineups { get; set; } = null!;
    public DbSet<AwayLineup> AwayLineups { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Score> Scores { get; set; } = null!;
    public DbSet<HomeStatistics> HomeStatistics { get; set; } = null!;
    public DbSet<AwayStatistics> AwayStatistics { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Venue> Venues { get; set; } = null!;
    public DbSet<DbUpdate> DbUpdates { get; set; } = null!;
}