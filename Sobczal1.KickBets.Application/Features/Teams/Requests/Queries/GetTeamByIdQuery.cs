using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Team;

namespace Sobczal1.KickBets.Application.Features.Teams.Requests.Queries;

public class GetTeamByIdQuery : IRequest<TeamDto>
{
    public int Id { get; set; }
}