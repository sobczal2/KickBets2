using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Bets;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Bets.Requests.Queries;
using Sobczal1.KickBets.Application.Models.Pagination;
using Sobczal1.KickBets.Domain.Bets;
using Sobczal1.KickBets.Domain.Identity;

namespace Sobczal1.KickBets.Application.Features.Bets.Handlers.Queries;

public class GetMyBetListQueryHandler : IRequestHandler<GetMyBetListQuery, PaginatedResponse<BaseBetDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public GetMyBetListQueryHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<BaseBetDto>> Handle(GetMyBetListQuery query,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.Email)
            ?.Value);
        if (user is null)
            throw new BadRequestException(new Dictionary<string, string> {{"User", "User not found"}});

        var bets = await _unitOfWork.BetRepository.GetAllWithRelatedData();

        bets = bets.Where(b => b.AppUserId == user.Id);

        var toResolveBets = await bets.Where(b => b.Status == "pending").ToListAsync();

        foreach (var toResolveBet in toResolveBets)
        {
            toResolveBet.TryResolving();
        }

        await _unitOfWork.Save();

        return await PaginatedResponse<BaseBetDto>.CreateAsync(
            bets.ProjectTo<BaseBetDto>(_mapper.ConfigurationProvider), query.PaginatedRequestData, q => q.TimeStamp,
            true);
    }
}