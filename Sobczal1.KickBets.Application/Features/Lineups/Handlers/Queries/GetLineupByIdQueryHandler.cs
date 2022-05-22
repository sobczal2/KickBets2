using AutoMapper;
using MediatR;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Football.Lineups;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Lineups.Requests.Queries;

namespace Sobczal1.KickBets.Application.Features.Lineups.Handlers.Queries;

public class GetLineupByIdQueryHandler : IRequestHandler<GetLineupByIdQuery, LineupDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLineupByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<LineupDto> Handle(GetLineupByIdQuery query, CancellationToken cancellationToken)
    {
        var lineup = await _unitOfWork.LineupRepository.Get(query.Id);
        if (lineup is null)
            throw new NotFoundException("id", $"Lineup with id: {query.Id} not found.");
        return _mapper.Map<LineupDto>(lineup);
    }
}