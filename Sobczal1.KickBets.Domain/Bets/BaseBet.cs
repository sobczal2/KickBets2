using Sobczal1.KickBets.Domain.Football;
using Sobczal1.KickBets.Domain.Identity;

namespace Sobczal1.KickBets.Domain.Bets;

public class BaseBet : BaseDomainEntity
{
    public double Value { get; set; }
    public string CreatedAt { get; set; } = null!;
    public string CreatedBy { get; set; } = null!;
    public int FixtureId { get; set; }
    public virtual Fixture Fixture { get; set; } = null!;
    public int AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; } = null!;
}