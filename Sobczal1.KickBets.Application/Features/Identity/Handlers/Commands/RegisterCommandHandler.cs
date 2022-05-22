using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Sobczal1.KickBets.Application.DTOs.Identity;
using Sobczal1.KickBets.Application.DTOs.Identity.Validators;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Identity.Requests.Commands;
using Sobczal1.KickBets.Application.Services.Identity;
using Sobczal1.KickBets.Domain.Identity;

namespace Sobczal1.KickBets.Application.Features.Identity.Handlers.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDto>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly TokenService _tokenService;

    public RegisterCommandHandler(UserManager<AppUser> userManager, IConfiguration configuration, IMapper mapper, TokenService tokenService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _mapper = mapper;
        _tokenService = tokenService;
    }
    public async Task<UserDto> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var validator = new RegisterDtoValidator(_userManager);
        var validationResult = await validator.ValidateAsync(command.RegisterDto, cancellationToken);
        if (validationResult.IsValid == false)
            throw new ValidationErrorsException(validationResult);

        var user = new AppUser
        {
            UserName = command.RegisterDto.UserName,
            Email = command.RegisterDto.Email,
            Balance = _configuration.GetValue<int>("UserSettings:StartingBalance"),
        };

        var result = await _userManager.CreateAsync(user, command.RegisterDto.Password);
        
        if (result.Succeeded)
        {
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);
            return userDto;
        }

        throw new BadRequestException(new Dictionary<string, string>
        {
            {"UserName", "Something went wrong, try again later."}, 
            {"Email", "Something went wrong, try again later."},
            {"Password", "Something went wrong, try again later."}
        });
    }
}