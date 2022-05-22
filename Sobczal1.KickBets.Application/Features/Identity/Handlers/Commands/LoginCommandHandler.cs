using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Sobczal1.KickBets.Application.DTOs.Identity;
using Sobczal1.KickBets.Application.DTOs.Identity.Validators;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Identity.Requests.Commands;
using Sobczal1.KickBets.Application.Services.Identity;
using Sobczal1.KickBets.Domain.Identity;

namespace Sobczal1.KickBets.Application.Features.Identity.Handlers.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, UserDto>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly TokenService _tokenService;

    public LoginCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper,
        TokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task<UserDto> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var validator = new LoginDtoValidator();
        var validationResult = await validator.ValidateAsync(command.LoginDto, cancellationToken);
        if (validationResult.IsValid == false)
            throw new ValidationErrorsException(validationResult);

        var user = await _userManager.FindByEmailAsync(command.LoginDto.Email);
        if (user == null)
            throw new BadRequestException(new Dictionary<string, string> {{"Email", "Invalid login and/or password"}, {"Password", "Invalid login and/or password"}});

        var result = await _signInManager.CheckPasswordSignInAsync(user, command.LoginDto.Password, false);

        if (result.Succeeded)
        {
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);
            return userDto;
        }

        throw new BadRequestException(new Dictionary<string, string> {{"Email", "Invalid login and/or password"}, {"Password", "Invalid login and/or password"}});
    }
}