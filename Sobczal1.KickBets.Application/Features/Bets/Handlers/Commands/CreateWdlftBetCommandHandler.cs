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

public class CreateWdlftBetCommandHandler : IRequestHandler<CreateWdlftBetCommand, Unit>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;

    public CreateWdlftBetCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,
        UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(CreateWdlftBetCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.Email)
            ?.Value);
        if (user is null)
            throw new BadRequestException(new Dictionary<string, string> {{"User", "User not found"}});

        var validator = new CreateWdlftBetDtoValidator(user, _unitOfWork.FixtureRepository);
        var validationResult = await validator.ValidateAsync(command.CreateWdlftBetDto, cancellationToken);
        if (validationResult.IsValid == false)
            throw new ValidationErrorsException(validationResult);

        var fixture = await _unitOfWork.FixtureRepository.GetWithBetsData(command.CreateWdlftBetDto.FixtureId);
        if (fixture is null)
            throw new BadRequestException(new Dictionary<string, string>
                {{"FixtureId", $"Fixture with id: {command.CreateWdlftBetDto.FixtureId} not found"}});

        // if(DateTime.Compare(fixture.Date, DateTime.Now) < 0)
        //     throw new BadRequestException(new Dictionary<string, string> {{"FixtureId", $"Fixture with id: {command.CreateWdlftBetDto.FixtureId} has already started"}});

        var wdlftBet = new WdlftBet
        {
            FixtureId = command.CreateWdlftBetDto.FixtureId,
            AppUserId = user.Id,
            Value = command.CreateWdlftBetDto.Value,
            Status = "pending",
            TimeStamp = DateTime.Now,
            WdlftSide = command.CreateWdlftBetDto.WdlftSide
        };

        switch (command.CreateWdlftBetDto.WdlftSide)
        {
            case "home":
                fixture.BetsData.WdlftBetsData.HomeBetsValue += command.CreateWdlftBetDto.Value;
                break;
            case "away":
                fixture.BetsData.WdlftBetsData.AwayBetsValue += command.CreateWdlftBetDto.Value;
                break;
            case "draw":
                fixture.BetsData.WdlftBetsData.DrawBetsValue += command.CreateWdlftBetDto.Value;
                break;
        }

        user.Balance -= command.CreateWdlftBetDto.Value;

        await _unitOfWork.BetRepository.Add(wdlftBet);
        await _unitOfWork.Save();

        return Unit.Value;
    }
}