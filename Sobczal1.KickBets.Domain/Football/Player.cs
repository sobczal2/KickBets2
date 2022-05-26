namespace Sobczal1.KickBets.Domain.Football;

public class Player : BaseDomainEntity
{
    public string Name { get; set; } = null!;
    public int Number { get; set; }
    public string? Pos { get; set; }
    public int? GridX { get; set; }
    public int? GridY { get; set; }
    public int LineupId { get; set; }
    public virtual Lineup Lineup { get; set; } = null!;
    public bool Starting11 { get; set; }
}