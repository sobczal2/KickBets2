namespace Sobczal1.KickBets.Domain.Fixtures;

public class Lineup : BaseDomainEntity
{
    public int TeamId { get; set; }
    public virtual Team Team { get; set; } = null!;
    public string Formation { get; set; } = null!;
    public virtual ICollection<Player> Start11 { get; set; } = null!;
    public virtual ICollection<Player> Substitutes { get; set; } = null!;
    public string CoachName { get; set; } = null!;
    public string Photo { get; set; } = null!;
    public int FixtureId { get; set; }
    public virtual Fixture Fixture { get; set; }
}