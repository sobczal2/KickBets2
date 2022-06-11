using FluentValidation;
using Sobczal1.KickBets.Application.Contracts.Persistence.Football;

namespace Sobczal1.KickBets.Application.DTOs.Football.Fixtures.Validators;

public class FixtureListParamsDtoValidator : AbstractValidator<FixtureListParams>
{
    public FixtureListParamsDtoValidator(ILeagueRepository leagueRepository)
    {
        RuleFor(p => p.Type)
            .Must(p => p is "all" or "upcoming" or "ended" or "cancelled")
            .WithMessage("{PropertyName} must be 'all', 'upcoming', 'cancelled' or 'ended'.");

        RuleFor(p => p.LeagueId)
            .MustAsync(async (p, cancellationToken) =>
            {
                if (!p.HasValue)
                    return true;
                return await leagueRepository.Exists(p.Value);
            })
            .WithMessage("League with id: {PropertyValue} not found.");
    }
}