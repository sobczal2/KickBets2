namespace Sobczal1.KickBets.Domain.Fixtures;

public class Player : BaseDomainEntity
{
    public string Name { get; set; } = null!;
    public int Number { get; set; }
    public string Pos { get; set; } = null!;
    public (int x, int y)? Grid { get; set; }
    public int LineupId { get; set; }
    public virtual Lineup Lineup { get; set; } = null!;
}