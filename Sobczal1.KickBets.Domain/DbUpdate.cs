namespace Sobczal1.KickBets.Domain;

public class DbUpdate : BaseDomainEntity
{
    public DbUpdate()
    {
        TimeStamp = DateTime.Now;
        LeaguesUpdate = false;
    }
    public DateTime TimeStamp { get; set; }
    public bool LeaguesUpdate { get; set; }
    public bool FixturesBigUpdate { get; set; }
    public bool FixturesSmallUpdate { get; set; }
    public bool StatisticsUpdate { get; set; }
    public bool LineupsUpdate { get; set; }
    public bool EventsUpdate { get; set; }
}