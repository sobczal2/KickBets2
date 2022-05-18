namespace Sobczal1.KickBets.Domain.Fixtures;

public class Team : BaseDomainEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Logo { get; set; } = null!;
}