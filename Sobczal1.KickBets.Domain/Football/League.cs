namespace Sobczal1.KickBets.Domain.Football;

public class League : BaseDomainEntity
{
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? Logo { get; set; }
    public string? Flag { get; set; }
    public int Season { get; set; }
}