using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Identity;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Identity.Requests.Commands;
using Sobczal1.KickBets.Domain.Identity;

namespace Sobczal1.KickBets.Application.Features.Identity.Handlers.Commands;

public class AddBalanceCommandHandler : IRequestHandler<AddBalanceCommand, UserDto>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public AddBalanceCommandHandler(UserManager<AppUser> userManager, IMapper mapper, IConfiguration configuration,  IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }
    public async Task<UserDto> Handle(AddBalanceCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)
            ?.Value);
        if (user is null)
            throw new BadRequestException(new Dictionary<string, string> {{"User", "User not found."}});

        if (DateTime.Compare(user.BalanceLastAddedAt.AddDays(1), DateTime.Now) > 0)
            throw new BadRequestException(new Dictionary<string, string> {{"Balance", "Balance addition not yet available."}});

        user.Balance += _configuration.GetValue<double>("UserSettings:AddBalanceValue");
        user.BalanceLastAddedAt = DateTime.Now;
        await _unitOfWork.Save();

        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }
}