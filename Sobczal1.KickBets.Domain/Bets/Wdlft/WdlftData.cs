namespace Sobczal1.KickBets.Domain.Bets.Wdlft;

public class WdlftData : BaseDomainEntity
{
    public WdlftData()
    {
        HomeBetsValue = 0;
        DrawBetsValue = 0;
        AwayBetsValue = 0;
    }
    public double HomeBetsValue { get; set; }
    public double DrawBetsValue { get; set; }
    public double AwayBetsValue { get; set; }
}