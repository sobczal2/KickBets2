using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sobczal1.KickBets.Domain.Identity;

namespace Sobczal1.KickBets.Application.DTOs.Identity.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator(UserManager<AppUser> userManager)
    {
        RuleFor(r => r.UserName)
            .NotEmpty()
            .MinimumLength(5)
            .MustAsync(async (username, cancellationToken) =>
            {
                return !await userManager.Users.AnyAsync(u => string.Equals(u.UserName, username, StringComparison.CurrentCultureIgnoreCase));
            })
            .WithMessage("User with {PropertyName} {PropertyValue} already exists");
        
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(async (email, cancellationToken) =>
            {
                return !await userManager.Users.AnyAsync(u => string.Equals(u.Email, email, StringComparison.CurrentCultureIgnoreCase));
            })
            .WithMessage("User with {PropertyName} {PropertyValue} already exists");

        RuleFor(r => r.Password)
            .NotEmpty()
            .MinimumLength(5);
    }
}