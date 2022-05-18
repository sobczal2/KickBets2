using Microsoft.EntityFrameworkCore;
using Sobczal1.KickBets.Domain.Bets;
using Sobczal1.KickBets.Domain.Fixtures;

namespace Sobczal1.KickBets.Persistence;

public class KickBetsDbContext : DbContext
{
    public KickBetsDbContext()
    {
        
    }

    public KickBetsDbContext(DbContextOptions<KickBetsDbContext> options) : base(options)
    {
        
    }

    public DbSet<BaseBet> Bets { get; set; }
    public DbSet<WdlftBet> WdlftBets { get; set; }
    public DbSet<WdlhtBet> WdlhtBets { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Fixture> Fixtures { get; set; }
    public DbSet<League> Leagues { get; set; }
    public DbSet<Lineup> Lineups { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Statistics> Statistics { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Venue> Venues { get; set; }
}