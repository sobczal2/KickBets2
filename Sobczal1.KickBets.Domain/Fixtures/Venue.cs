namespace Sobczal1.KickBets.Domain.Fixtures;

public class Venue : BaseDomainEntity
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public int Capacity { get; set; }
    public string Surface { get; set; } = null!;
    public string Image { get; set; } = null!;
}