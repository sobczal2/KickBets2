using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Leagues;
using Sobczal1.KickBets.Application.Models.Pagination;

namespace Sobczal1.KickBets.Application.Features.Leagues.Requests.Queries;

public class GetLeagueListQuery : IRequest<PaginatedResponse<LeagueDto>>
{
    public PaginatedRequestData PaginatedRequestData { get; set; } = null!;
}