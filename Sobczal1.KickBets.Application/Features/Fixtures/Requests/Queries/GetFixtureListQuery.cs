using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Fixtures;
using Sobczal1.KickBets.Application.Models.Pagination;

namespace Sobczal1.KickBets.Application.Features.Fixtures.Requests.Queries;

public class GetFixtureListQuery : IRequest<PaginatedResponse<FixtureDto>>
{
    public PaginatedRequestData PaginatedRequestData { get; set; } = null!;
    public FixtureListParams FixtureListParams { get; set; } = null!;
}