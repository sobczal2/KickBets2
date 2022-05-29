namespace Sobczal1.KickBets.Application.DTOs.Bets;

public class BetsDataDto : BaseDto
{
    public WdlhtBetsDataDto WdlhtBetsData { get; set; } = null!;
    public WdlftBetsDataDto WdlftBetsData { get; set; } = null!;
}