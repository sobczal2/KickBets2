using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Football.Leagues;
using Sobczal1.KickBets.Application.Features.Leagues.Requests.Queries;
using Sobczal1.KickBets.Application.Models.Pagination;

namespace Sobczal1.KickBets.Application.Features.Leagues.Handlers.Queries;

public class GetLeagueListQueryHandler : IRequestHandler<GetLeagueListQuery, PaginatedResponse<LeagueDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLeagueListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<PaginatedResponse<LeagueDto>> Handle(GetLeagueListQuery query, CancellationToken cancellationToken)
    {
        var leagues = await _unitOfWork.LeagueRepository.GetAll();
        return await PaginatedResponse<LeagueDto>.CreateAsync(
            leagues.ProjectTo<LeagueDto>(_mapper.ConfigurationProvider), query.PaginatedRequestData, q => q.Name);
    }
}