using MediatR;
using Sobczal1.KickBets.Application.DTOs.Bets;
using Sobczal1.KickBets.Application.Models.Pagination;

namespace Sobczal1.KickBets.Application.Features.Bets.Requests.Queries;

public class GetMyBetListQuery : IRequest<PaginatedResponse<BaseBetDto>>
{
    public PaginatedRequestData PaginatedRequestData { get; set; } = null!;
}