using Microsoft.AspNetCore.Identity;

namespace Sobczal1.KickBets.Domain.Identity;

public class AppUser : IdentityUser<int>
{
    public double Balance { get; set; }
    public DateTime BalanceLastAddedAt { get; set; }
}