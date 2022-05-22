namespace Sobczal1.KickBets.Domain.Football;

public class Team : BaseDomainEntity
{
    public string Name { get; set; } = null!;
    public string? Logo { get; set; }
}