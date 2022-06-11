using Sobczal1.KickBets.Domain.Bets;

namespace Sobczal1.KickBets.Domain.Football;

public class Fixture : BaseDomainEntity
{
    public string? Referee { get; set; }
    public DateTime Date { get; set; }
    public int? VenueId { get; set; }
    public virtual Venue? Venue { get; set; }
    public int StatusId { get; set; }
    public virtual Status Status { get; set; } = null!;
    public int LeagueId { get; set; }
    public virtual League League { get; set; } = null!;
    public int HomeTeamId { get; set; }
    public virtual Team HomeTeam { get; set; } = null!;
    public int AwayTeamId { get; set; }
    public virtual Team AwayTeam { get; set; } = null!;
    public int? HomeLineupId { get; set; }
    public HomeLineup? HomeLineup { get; set; }
    public int? AwayLineupId { get; set; }
    public AwayLineup? AwayLineup { get; set; }
    public int ScoreId { get; set; }
    public virtual Score Score { get; set; } = null!;
    public int? HomeStatisticsId { get; set; }
    public virtual HomeStatistics? HomeStatistics { get; set; }
    public int? AwayStatisticsId { get; set; }
    public virtual AwayStatistics? AwayStatistics { get; set; }
    public virtual ICollection<HomeEvent> HomeEvents { get; set; } = null!;
    public virtual ICollection<AwayEvent> AwayEvents { get; set; } = null!;
    public virtual ICollection<BaseBet> Bets { get; set; } = null!;
    public int BetsDataId { get; set; }
    public BetsData BetsData { get; set; } = null!;
}