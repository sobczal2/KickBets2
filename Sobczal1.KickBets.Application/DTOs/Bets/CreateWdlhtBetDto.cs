namespace Sobczal1.KickBets.Application.DTOs.Bets;

public class CreateWdlhtBetDto
{
    public int FixtureId { get; set; }
    public double Value { get; set; }
    // "home", "away" or "draw"
    public string WdlhtSide { get; set; } = null!;
}