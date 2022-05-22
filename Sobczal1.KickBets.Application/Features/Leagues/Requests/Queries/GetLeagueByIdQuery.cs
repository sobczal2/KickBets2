using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Leagues;

namespace Sobczal1.KickBets.Application.Features.Leagues.Requests.Queries;

public class GetLeagueByIdQuery : IRequest<LeagueDto>
{
    public int Id { get; set; }
}