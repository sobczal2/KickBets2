using AutoMapper;
using MediatR;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Football.Statistics;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Statistics.Requests.Queries;

namespace Sobczal1.KickBets.Application.Features.Statistics.Handlers.Queries;

public class GetStatisticsByIdQueryHandler : IRequestHandler<GetStatisticsByIdQuery, StatisticDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetStatisticsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<StatisticDto> Handle(GetStatisticsByIdQuery query, CancellationToken cancellationToken)
    {
        var statistics = await _unitOfWork.StatisticsRepository.Get(query.Id);
        if (statistics is null)
            throw new NotFoundException("id", $"Statistics with id: {query.Id} not found.");
        return _mapper.Map<StatisticDto>(statistics);
    }
}