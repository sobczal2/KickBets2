namespace Sobczal1.KickBets.Domain.Bets;

public class WdlhtBetsData : BaseDomainEntity
{
    public WdlhtBetsData()
    {
        HomeBetsValue = 0;
        DrawBetsValue = 0;
        AwayBetsValue = 0;
    }

    public double HomeBetsValue { get; set; }
    public double DrawBetsValue { get; set; }
    public double AwayBetsValue { get; set; }

    public double GetHomeBetsMultiplier()
    {
        if (HomeBetsValue == 0)
            return 1;
        return 1 + (DrawBetsValue + AwayBetsValue) / HomeBetsValue;
    }

    public double GetDrawBetsMultiplier()
    {
        if (DrawBetsValue == 0)
            return 1;
        return 1 + (HomeBetsValue + AwayBetsValue) / DrawBetsValue;
    }

    public double GetAwayBetsMultiplier()
    {
        if (AwayBetsValue == 0)
            return 1;
        return 1 + (HomeBetsValue + DrawBetsValue) / AwayBetsValue;
    }
}