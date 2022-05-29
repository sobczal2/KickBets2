namespace Sobczal1.KickBets.Domain.Bets;

public class WdlhtBet : BaseBet
{
    // "home", "away" or "draw"
    public string WdlhtSide { get; set; } = null!;
    
    public override string GetBetType()
    {
        switch (WdlhtSide)
        {
            case "home":
                return "wdlht:home";
            case "away":
                return "wdlht:away";
            case "draw":
                return "wdlht:draw";
            default:
                throw new InvalidDataException("Bet record corrupted.");
        }
    }
    
    public override void TryResolving()
    {
        if (Status != "pending") return;
        var endedStatuses = new[] {"FT", "AET", "PEN"};
        var cancelledStatuses = new[] {"SUSP", "CANC", "ABD", "AWD", "WO"};

        if (endedStatuses.Contains(Fixture.Status.Short))
        {
            switch (WdlhtSide)
            {
                case "home":
                    if (Fixture.Score.HomeHalfTime > Fixture.Score.AwayHalfTime)
                    {
                        Status = "won";
                        AppUser.Balance += Fixture.BetsData.WdlhtBetsData.GetHomeBetsMultiplier() * Value;
                    }
                    else
                    {
                        Status = "lost";
                    }
                    break;
                case "away":
                    if (Fixture.Score.HomeHalfTime < Fixture.Score.AwayHalfTime)
                    {
                        Status = "won";
                        AppUser.Balance += Fixture.BetsData.WdlhtBetsData.GetAwayBetsMultiplier() * Value;
                    }
                    else
                    {
                        Status = "lost";
                    }
                    break;
                case "draw":
                    if (Fixture.Score.HomeHalfTime == Fixture.Score.AwayHalfTime)
                    {
                        Status = "won";
                        AppUser.Balance += Fixture.BetsData.WdlhtBetsData.GetDrawBetsMultiplier() * Value;
                    }
                    else
                    {
                        Status = "lost";
                    }
                    break;
            }
        }

        if (cancelledStatuses.Contains(Fixture.Status.Short))
        {
            Status = "cancelled";
            AppUser.Balance += Value;
        }
    }
}