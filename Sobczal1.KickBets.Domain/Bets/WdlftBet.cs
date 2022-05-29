namespace Sobczal1.KickBets.Domain.Bets;

public class WdlftBet : BaseBet
{
    // "home", "away" or "draw"
    public string WdlftSide { get; set; } = null!;

    public override string GetBetType()
    {
        switch (WdlftSide)
        {
            case "home":
                return "wdlft:home";
            case "away":
                return "wdlft:away";
            case "draw":
                return "wdlft:draw";
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
            switch (WdlftSide)
            {
                case "home":
                    if (Fixture.Score.HomeFullTime > Fixture.Score.AwayFullTime)
                    {
                        Status = "won";
                        AppUser.Balance += Fixture.BetsData.WdlftBetsData.GetHomeBetsMultiplier() * Value;
                    }
                    else
                    {
                        Status = "lost";
                    }
                    break;
                case "away":
                    if (Fixture.Score.HomeFullTime < Fixture.Score.AwayFullTime)
                    {
                        Status = "won";
                        AppUser.Balance += Fixture.BetsData.WdlftBetsData.GetAwayBetsMultiplier() * Value;
                    }
                    else
                    {
                        Status = "lost";
                    }
                    break;
                case "draw":
                    if (Fixture.Score.HomeFullTime == Fixture.Score.AwayFullTime)
                    {
                        Status = "won";
                        AppUser.Balance += Fixture.BetsData.WdlftBetsData.GetDrawBetsMultiplier() * Value;
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