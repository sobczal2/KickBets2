using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sobczal1.KickBets.Application.DTOs.Identity;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Identity.Requests.Queries;
using Sobczal1.KickBets.Application.Services.Identity;
using Sobczal1.KickBets.Domain.Identity;

namespace Sobczal1.KickBets.Application.Features.Identity.Handlers.Queries;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly TokenService _tokenService;

    public GetCurrentUserQueryHandler(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper, TokenService tokenService)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _tokenService = tokenService;
    }
    public async Task<UserDto> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)
            ?.Value);
        if (user is null)
            throw new BadRequestException(new Dictionary<string, string> {{"User", "User not found."}});
        
        var userDto = _mapper.Map<UserDto>(user);
        if (query.RefreshToken.HasValue && query.RefreshToken.Value)
            userDto.Token = _tokenService.CreateToken(user);
        return userDto;
    }
}