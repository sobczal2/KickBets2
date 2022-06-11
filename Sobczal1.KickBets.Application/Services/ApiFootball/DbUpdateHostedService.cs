using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Domain;

namespace Sobczal1.KickBets.Application.Services.ApiFootball;

public class DbUpdateHostedService : IHostedService, IDisposable
{
    private readonly ILogger<DbUpdateHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Timer _timer = null!;

    public DbUpdateHostedService(ILogger<DbUpdateHostedService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service started");


        //TODO change this for updates
        _timer = new Timer(PerformUpdates, null, TimeSpan.FromMinutes(2.5),
            TimeSpan.FromMinutes(5));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service stopped");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private async void PerformUpdates(object? state)
    {
        _logger.LogWarning("DbUpdate started");
        var sw = Stopwatch.StartNew();
        using var scope = _serviceProvider.CreateScope();
        var dbUpdateRepository = scope.ServiceProvider.GetRequiredService<IDbUpdateRepository>();
        var footballApiService = scope.ServiceProvider.GetRequiredService<FootballApiService>();
        var dbUpdate = new DbUpdate();
        if (await dbUpdateRepository.ShouldPerformLeaguesUpdate())
            try
            {
                dbUpdate.LeaguesUpdate = true;
                await footballApiService.UpdateTrackedLeagues();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to update leagues");
            }
        else
            dbUpdate.LeaguesUpdate = false;

        if (await dbUpdateRepository.ShouldPerformFixturesBigUpdate())
            try
            {
                dbUpdate.FixturesBigUpdate = true;
                await footballApiService.UpdateFixtures(DateTime.Now.AddDays(-30), DateTime.Now.AddDays(30));
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to update fixtures");
            }
        else
            dbUpdate.FixturesBigUpdate = false;

        if (await dbUpdateRepository.ShouldPerformFixturesSmallUpdate())
            try
            {
                dbUpdate.FixturesSmallUpdate = true;
                await footballApiService.UpdateFixtures(DateTime.Now.AddHours(-5), DateTime.Now.AddHours(5));
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to update fixtures");
            }
        else
            dbUpdate.FixturesSmallUpdate = false;

        if (await dbUpdateRepository.ShouldPerformStatisticsUpdate())
            try
            {
                dbUpdate.StatisticsUpdate = true;
                await footballApiService.UpdateStatistics();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to update statistics");
            }
        else
            dbUpdate.StatisticsUpdate = false;

        if (await dbUpdateRepository.ShouldPerformLineupsUpdate())
            try
            {
                dbUpdate.LineupsUpdate = true;
                await footballApiService.UpdateLineups();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to update lineups");
            }
        else
            dbUpdate.LineupsUpdate = false;

        if (await dbUpdateRepository.ShouldPerformEventsUpdate())
            try
            {
                dbUpdate.EventsUpdate = true;
                await footballApiService.UpdateEvents();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to update events");
            }
        else
            dbUpdate.EventsUpdate = false;

        await dbUpdateRepository.OnUpdatePerformed(dbUpdate);

        sw.Stop();
        _logger.LogWarning($"Db update took {sw.Elapsed.TotalSeconds} seconds");
    }
}