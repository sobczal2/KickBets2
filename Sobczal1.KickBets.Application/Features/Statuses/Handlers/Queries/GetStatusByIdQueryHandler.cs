using AutoMapper;
using MediatR;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Football.Status;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Statuses.Requests.Queries;

namespace Sobczal1.KickBets.Application.Features.Statuses.Handlers.Queries;

public class GetStatusByIdQueryHandler : IRequestHandler<GetStatusByIdQuery, StatusDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetStatusByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<StatusDto> Handle(GetStatusByIdQuery query, CancellationToken cancellationToken)
    {
        var status = await _unitOfWork.StatusRepository.Get(query.Id);
        if (status is null)
            throw new NotFoundException("id", $"Status with id: {query.Id} not found.");
        return _mapper.Map<StatusDto>(status);
    }
}