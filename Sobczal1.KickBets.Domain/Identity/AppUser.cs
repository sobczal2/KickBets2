using Microsoft.AspNetCore.Identity;
using Sobczal1.KickBets.Domain.Bets;

namespace Sobczal1.KickBets.Domain.Identity;

public class AppUser : IdentityUser<int>
{
    public double Balance { get; set; }
    public virtual ICollection<BaseBet> Bets { get; set; } = null!;
    public DateTime BalanceLastAddedAt { get; set; }
}