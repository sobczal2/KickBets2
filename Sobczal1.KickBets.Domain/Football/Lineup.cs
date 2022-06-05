namespace Sobczal1.KickBets.Domain.Football;

public class Lineup : BaseDomainEntity
{
    public int TeamId { get; set; }
    public virtual Team Team { get; set; } = null!;
    public string? Formation { get; set; } = null!;
    public virtual ICollection<Player> Players { get; set; } = null!;
    public string? CoachName { get; set; }
    public string? CoachPhoto { get; set; }
    public virtual Fixture Fixture { get; set; } = null!;
}