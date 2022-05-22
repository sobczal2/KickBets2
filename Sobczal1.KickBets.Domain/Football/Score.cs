namespace Sobczal1.KickBets.Domain.Football;

public class Score : BaseDomainEntity
{
    public virtual Fixture Fixture { get; set; }
    public int? HomeCurrentScore { get; set; }
    public int? AwayCurrentScore { get; set; }
    public int? HomeHalfTime { get; set; }
    public int? AwayHalfTime { get; set; }
    public int? HomeFullTime { get; set; }
    public int? AwayFullTime { get; set; }
    public int? HomeExtraTime { get; set; }
    public int? AwayExtraTime { get; set; }
    public int? HomePenalty { get; set; }
    public int? AwayPenalty { get; set; }
}