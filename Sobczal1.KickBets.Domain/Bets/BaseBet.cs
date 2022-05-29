using Sobczal1.KickBets.Domain.Football;
using Sobczal1.KickBets.Domain.Identity;

namespace Sobczal1.KickBets.Domain.Bets;

public class BaseBet : BaseDomainEntity
{
    public int FixtureId { get; set; }
    public Fixture Fixture { get; set; } = null!;
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    public double Value { get; set; }
    // "pending", "won", "lost" or "cancelled"
    public string Status { get; set; } = null!;
    public DateTime TimeStamp { get; set; }

    public virtual string GetBetType()
    {
        throw new NotSupportedException("Description of base bet not available.");
    }

    public virtual void TryResolving()
    {
        throw new NotSupportedException("Resolving base bet not available.");
    }
}