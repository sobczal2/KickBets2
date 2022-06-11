using AutoMapper;
using MediatR;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Football.Scores;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Scores.Requests.Queries;

namespace Sobczal1.KickBets.Application.Features.Scores.Handlers.Queries;

public class GetScoreByIdQueryHandler : IRequestHandler<GetScoreByIdQuery, ScoreDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetScoreByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ScoreDto> Handle(GetScoreByIdQuery query, CancellationToken cancellationToken)
    {
        var score = await _unitOfWork.ScoreRepository.Get(query.Id);
        if (score is null)
            throw new NotFoundException("id", $"Score with id: {query.Id} not found.");
        return _mapper.Map<ScoreDto>(score);
    }
}