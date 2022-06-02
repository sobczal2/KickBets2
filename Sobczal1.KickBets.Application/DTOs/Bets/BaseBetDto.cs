namespace Sobczal1.KickBets.Application.DTOs.Bets;

public class BaseBetDto : BaseDto
{
    public int FixtureId { get; set; }
    public double Value { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Status { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int BetsDataId { get; set; }
    public string HomeTeamName { get; set; } = null!;
    public string AwayTeamName { get; set; } = null!;
    public string Description { get; set; } = null!;
}