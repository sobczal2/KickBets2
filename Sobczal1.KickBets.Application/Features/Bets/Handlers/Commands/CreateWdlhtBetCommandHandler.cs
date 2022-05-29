using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Bets.Validators;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Bets.Requests.Commands;
using Sobczal1.KickBets.Domain.Bets;
using Sobczal1.KickBets.Domain.Identity;

namespace Sobczal1.KickBets.Application.Features.Bets.Handlers.Commands;

public class CreateWdlhtBetCommandHandler : IRequestHandler<CreateWdlhtBetCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public CreateWdlhtBetCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }
    
    public async Task<Unit> Handle(CreateWdlhtBetCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)
            ?.Value);
        if (user is null)
            throw new BadRequestException(new Dictionary<string, string> {{"User", "User not found"}});
        
        var validator = new CreateWdlhtBetDtoValidator(user, _unitOfWork.FixtureRepository);
        var validationResult = await validator.ValidateAsync(command.CreateWdlhtBetDto, cancellationToken);
        if (validationResult.IsValid == false)
            throw new ValidationErrorsException(validationResult);
        
        var fixture = await _unitOfWork.FixtureRepository.GetWithBetsData(command.CreateWdlhtBetDto.FixtureId);
        if(fixture is null)
            throw new BadRequestException(new Dictionary<string, string> {{"FixtureId", $"Fixture with id: {command.CreateWdlhtBetDto.FixtureId} not found"}});
        
        if(DateTime.Compare(fixture.Date, DateTime.Now) < 0)
            throw new BadRequestException(new Dictionary<string, string> {{"FixtureId", $"Fixture with id: {command.CreateWdlhtBetDto.FixtureId} has already started"}});

        var wdlhtBet = new WdlhtBet
        {
            FixtureId = command.CreateWdlhtBetDto.FixtureId,
            AppUserId = user.Id,
            Value = command.CreateWdlhtBetDto.Value,
            Status = "pending",
            TimeStamp = DateTime.Now,
            WdlhtSide = command.CreateWdlhtBetDto.WdlhtSide,
        };
        
        switch (command.CreateWdlhtBetDto.WdlhtSide)
        {
            case "home":
                fixture.BetsData.WdlhtBetsData.HomeBetsValue += command.CreateWdlhtBetDto.Value;
                break;
            case "away":
                fixture.BetsData.WdlhtBetsData.AwayBetsValue += command.CreateWdlhtBetDto.Value;
                break;
            case "draw":
                fixture.BetsData.WdlhtBetsData.DrawBetsValue += command.CreateWdlhtBetDto.Value;
                break;
        }

        user.Balance -= command.CreateWdlhtBetDto.Value;

        await _unitOfWork.BetRepository.Add(wdlhtBet);
        await _unitOfWork.Save();
        
        return Unit.Value;
    }
}