using FluentValidation;
using Sobczal1.KickBets.Application.Contracts.Persistence.Football;
using Sobczal1.KickBets.Domain.Identity;

namespace Sobczal1.KickBets.Application.DTOs.Bets.Validators;

public class CreateWdlftBetDtoValidator : AbstractValidator<CreateWdlftBetDto>
{
    public CreateWdlftBetDtoValidator(AppUser user, IFixtureRepository fixtureRepository)
    {
        RuleFor(b => b.FixtureId)
            .MustAsync(async (fixtureId, _) => await fixtureRepository.Exists(fixtureId))
            .WithMessage("Fixture with id: {PropertyValue} not found.");

        RuleFor(b => b.Value)
            .GreaterThan(0)
            .Must((_, value) => value <= user.Balance)
            .WithMessage("Not enough balance.");
        
        RuleFor(b => b.WdlftSide)
            .Must((_, wdlftSide) => wdlftSide is "home" or "away" or "draw")
            .WithMessage("{PropertyName} must be 'home', 'away' or 'draw'.");
    }
}