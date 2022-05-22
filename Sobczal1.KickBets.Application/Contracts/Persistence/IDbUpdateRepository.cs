using Sobczal1.KickBets.Domain;

namespace Sobczal1.KickBets.Application.Contracts.Persistence;

public interface IDbUpdateRepository
{
    Task OnUpdatePerformed(DbUpdate dbUpdate);
    Task<bool> ShouldPerformLeaguesUpdate();
    Task<bool> ShouldPerformFixturesSmallUpdate();
    Task<bool> ShouldPerformFixturesBigUpdate();
    Task<bool> ShouldPerformStatisticsUpdate();
    Task<bool> ShouldPerformLineupsUpdate();
    Task<bool> ShouldPerformEventsUpdate();
}