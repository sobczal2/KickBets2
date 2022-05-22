using MediatR;
using Sobczal1.KickBets.Application.DTOs.Identity;

namespace Sobczal1.KickBets.Application.Features.Identity.Requests.Commands;

public class RegisterCommand : IRequest<UserDto>
{
    public RegisterDto RegisterDto { get; set; } = null!;
}