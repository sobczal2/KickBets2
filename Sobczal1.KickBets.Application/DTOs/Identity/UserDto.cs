namespace Sobczal1.KickBets.Application.DTOs.Identity;

public class UserDto
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Balance { get; set; } = null!;
    public DateTime BalanceAddAvailableAt { get; set; }
    public string? Token { get; set; }
}