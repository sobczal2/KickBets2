using FluentValidation;

namespace Sobczal1.KickBets.Application.DTOs.Identity.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(p => p.Password)
            .NotEmpty()
            .MinimumLength(5);
    }
}