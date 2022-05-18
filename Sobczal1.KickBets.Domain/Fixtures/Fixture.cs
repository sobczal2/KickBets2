namespace Sobczal1.KickBets.Domain.Fixtures;

public class Fixture : BaseDomainEntity
{
    public string? Referee { get; set; }
    public DateTime Date { get; set; }
    public int VenueId { get; set; }
    public virtual Venue Venue { get; set; } = null!;
    public int StatusId { get; set; }
    public virtual Status Status { get; set; } = null!;
    public int LeagueId { get; set; }
    public virtual League League { get; set; } = null!;
    public int HomeTeamId { get; set; }
    public virtual Team HomeTeam { get; set; } = null!;
    public int AwayTeamId { get; set; }
    public virtual Team AwayTeam { get; set; } = null!;
    public int CurrentScoreId { get; set; }
    public virtual Score CurrentScore { get; set; } = null!;
    public int HalfTimeScoreId { get; set; }
    public virtual Score HalfTimeScore { get; set; } = null!;
    public int FullTimeScoreId { get; set; }
    public virtual Score FullTimeScore { get; set; } = null!;
    public int ExtraTimeScoreId { get; set; }
    public virtual Score ExtraTimeScore { get; set; } = null!;
    public int PenaltyScoreId { get; set; }
    public virtual Score PenaltyScore { get; set; } = null!;
    public int HomeTeamStatisticsId { get; set; }
    public virtual Statistics HomeTeamStatistics { get; set; } = null!;
    public int AwayTeamStatisticsId { get; set; }
    public virtual Statistics AwayTeamStatistics { get; set; } = null!;
    public virtual ICollection<Event> Events { get; set; } = null!;
}