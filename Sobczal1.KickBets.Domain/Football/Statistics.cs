namespace Sobczal1.KickBets.Domain.Football;

public class Statistics : BaseDomainEntity
{
    public virtual Fixture Fixture { get; set; } = null!;
    public int? ShotsOnGoal { get; set; }
    public int? ShotsOffGoal { get; set; }
    public int? ShotsInsideBox { get; set; }
    public int? ShotsOutsideBox { get; set; }
    public int? TotalShots { get; set; }
    public int? BlockedShots { get; set; }
    public int? Fouls { get; set; }
    public int? CornerKicks { get; set; }
    public int? Offsides { get; set; }
    public double? Possession { get; set; }
    public int? YellowCards { get; set; }
    public int? RedCards { get; set; }
    public int? GoalkeeperSaves { get; set; }
    public int? TotalPasses { get; set; }
    public int? AccuratePasses { get; set; }
    public double? Passes { get; set; }
}