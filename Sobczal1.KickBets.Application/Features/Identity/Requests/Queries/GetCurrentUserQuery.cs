using MediatR;
using Sobczal1.KickBets.Application.DTOs.Identity;

namespace Sobczal1.KickBets.Application.Features.Identity.Requests.Queries;

public class GetCurrentUserQuery : IRequest<UserDto>
{
    public bool? RefreshToken { get; set; }
}