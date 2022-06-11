using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Football.Players;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Players.Requests.Queries;

namespace Sobczal1.KickBets.Application.Features.Players.Handlers.Queries;

public class GetPlayersByLineupIdQueryHandler : IRequestHandler<GetPlayersByLineupIdQuery, List<PlayerDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPlayersByLineupIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<PlayerDto>> Handle(GetPlayersByLineupIdQuery query, CancellationToken cancellationToken)
    {
        if (!query.LineupId.HasValue)
            throw new BadRequestException(new Dictionary<string, string> {{"LineupId", "LineupId must not be null."}});
        if (!await _unitOfWork.LineupRepository.Exists(query.LineupId.Value))
            throw new NotFoundException("LineupId", $"Lineup with id: {query.LineupId} not found.");

        var players = await (await _unitOfWork.PlayerRepository.GetAll()).Where(p => p.LineupId == query.LineupId)
            .ToListAsync();

        return _mapper.Map<List<PlayerDto>>(players);
    }
}