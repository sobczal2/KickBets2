using MediatR;
using Sobczal1.KickBets.Application.DTOs.Identity;

namespace Sobczal1.KickBets.Application.Features.Identity.Requests.Commands;

public class LoginCommand : IRequest<UserDto>
{
    public LoginDto LoginDto { get; set; } = null!;
}