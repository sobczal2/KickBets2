using AutoMapper;
using MediatR;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Football.Leagues;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Leagues.Requests.Queries;

namespace Sobczal1.KickBets.Application.Features.Leagues.Handlers.Queries;

public class GetLeagueByIdQueryHandler : IRequestHandler<GetLeagueByIdQuery, LeagueDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLeagueByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<LeagueDto> Handle(GetLeagueByIdQuery query, CancellationToken cancellationToken)
    {
        var league = await _unitOfWork.LeagueRepository.Get(query.Id);
        if (league is null)
            throw new NotFoundException("id", $"League with id: {query.Id} not found.");
        return _mapper.Map<LeagueDto>(league);
    }
}