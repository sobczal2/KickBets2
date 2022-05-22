using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Lineups;

namespace Sobczal1.KickBets.Application.Features.Lineups.Requests.Queries;

public class GetLineupByIdQuery : IRequest<LineupDto>
{
    public int Id { get; set; }
}