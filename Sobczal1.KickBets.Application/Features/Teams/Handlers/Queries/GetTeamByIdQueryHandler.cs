using AutoMapper;
using MediatR;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Football.Team;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Teams.Requests.Queries;

namespace Sobczal1.KickBets.Application.Features.Teams.Handlers.Queries;

public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, TeamDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTeamByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TeamDto> Handle(GetTeamByIdQuery query, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.Get(query.Id);
        if (team is null)
            throw new NotFoundException("id", $"Team with id: {query.Id} not found.");
        return _mapper.Map<TeamDto>(team);
    }
}