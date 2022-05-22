using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Domain;

namespace Sobczal1.KickBets.Persistence.Repositories;

public class DbUpdateRepository : IDbUpdateRepository
{
    private readonly KickBetsDbContext _context;
    private readonly IConfiguration _configuration;

    public DbUpdateRepository(KickBetsDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    public async Task OnUpdatePerformed(DbUpdate dbUpdate)
    {
        _context.DbUpdates.Add(dbUpdate);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ShouldPerformLeaguesUpdate()
    {
        var updates = _context.DbUpdates.OrderByDescending(x => x.TimeStamp);
        var leagueUpdate = await updates.FirstOrDefaultAsync(x => x.LeaguesUpdate);
        var sinceLeagueUpdate = leagueUpdate is null ? TimeSpan.MaxValue : DateTime.Now - leagueUpdate.TimeStamp;
        return sinceLeagueUpdate.TotalMinutes > _configuration.GetValue<int>("DbUpdates:Leagues");
    }
    
    public async Task<bool> ShouldPerformFixturesSmallUpdate()
    {
        var updates = _context.DbUpdates.OrderByDescending(x => x.TimeStamp);
        var fixtureSmallUpdate = await updates.FirstOrDefaultAsync(x => x.FixturesSmallUpdate);
        var sinceFixturesSmallUpdate = fixtureSmallUpdate is null ? TimeSpan.MaxValue : DateTime.Now - fixtureSmallUpdate.TimeStamp;
        return sinceFixturesSmallUpdate.TotalMinutes > _configuration.GetValue<int>("DbUpdates:FixturesSmall");
    }
    
    public async Task<bool> ShouldPerformFixturesBigUpdate()
    {
        var updates = _context.DbUpdates.OrderByDescending(x => x.TimeStamp);
        var fixtureBigUpdate = await updates.FirstOrDefaultAsync(x => x.FixturesBigUpdate);
        var sinceFixturesBigUpdate = fixtureBigUpdate is null ? TimeSpan.MaxValue : DateTime.Now - fixtureBigUpdate.TimeStamp;
        return sinceFixturesBigUpdate.TotalMinutes > _configuration.GetValue<int>("DbUpdates:FixturesBig");
    }

    public async Task<bool> ShouldPerformStatisticsUpdate()
    {
        var updates = _context.DbUpdates.OrderByDescending(x => x.TimeStamp);
        var statisticsUpdate = await updates.FirstOrDefaultAsync(x => x.StatisticsUpdate);
        var sinceStatisticsUpdate = statisticsUpdate is null ? TimeSpan.MaxValue : DateTime.Now - statisticsUpdate.TimeStamp;
        return sinceStatisticsUpdate.TotalMinutes > _configuration.GetValue<int>("DbUpdates:Statistics");
    }

    public async Task<bool> ShouldPerformLineupsUpdate()
    {
        var updates = _context.DbUpdates.OrderByDescending(x => x.TimeStamp);
        var lineupsUpdate = await updates.FirstOrDefaultAsync(x => x.LineupsUpdate);
        var sinceLineupsUpdate = lineupsUpdate is null ? TimeSpan.MaxValue : DateTime.Now - lineupsUpdate.TimeStamp;
        return sinceLineupsUpdate.TotalMinutes > _configuration.GetValue<int>("DbUpdates:Lineups");
    }
    
    public async Task<bool> ShouldPerformEventsUpdate()
    {
        var updates = _context.DbUpdates.OrderByDescending(x => x.TimeStamp);
        var eventsUpdate = await updates.FirstOrDefaultAsync(x => x.EventsUpdate);
        var sinceEventsUpdate = eventsUpdate is null ? TimeSpan.MaxValue : DateTime.Now - eventsUpdate.TimeStamp;
        return sinceEventsUpdate.TotalMinutes > _configuration.GetValue<int>("DbUpdates:Events");
    }
}