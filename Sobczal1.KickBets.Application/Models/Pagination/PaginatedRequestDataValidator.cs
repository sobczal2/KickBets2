using FluentValidation;

namespace Sobczal1.KickBets.Application.Models.Pagination;

public class PaginatedRequestDataValidator : AbstractValidator<PaginatedRequestData>
{
    public PaginatedRequestDataValidator()
    {
        RuleFor(p => p.CurrentPage)
            .NotNull()
            .GreaterThan(0);
        RuleFor(p => p.PageSize)
            .NotNull()
            .GreaterThan(0);
    }
}