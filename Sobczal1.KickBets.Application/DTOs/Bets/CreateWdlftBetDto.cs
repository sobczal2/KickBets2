namespace Sobczal1.KickBets.Application.DTOs.Bets;

public class CreateWdlftBetDto
{
    public int FixtureId { get; set; }
    public double Value { get; set; }
    // "home", "away" or "draw"
    public string WdlftSide { get; set; } = null!;
}