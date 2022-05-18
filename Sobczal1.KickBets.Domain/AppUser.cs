using Microsoft.AspNetCore.Identity;
using Sobczal1.KickBets.Domain.Bets;

namespace Sobczal1.KickBets.Domain;

public class AppUser : IdentityUser<int>
{
    public double Balance { get; set; }
    public virtual ICollection<BaseBet> Bets { get; set; }
}