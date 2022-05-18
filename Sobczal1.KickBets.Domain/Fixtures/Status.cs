namespace Sobczal1.KickBets.Domain.Fixtures;

public class Status : BaseDomainEntity
{
    public string Long { get; set; } = null!;
    public string Short { get; set; } = null!;
    public int Elapsed { get; set; }
    public int FixtureId { get; set; }
    public virtual Fixture Fixture { get; set; } = null!;
}