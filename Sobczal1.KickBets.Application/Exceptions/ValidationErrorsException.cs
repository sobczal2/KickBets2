using System.Net;
using FluentValidation.Results;

namespace Sobczal1.KickBets.Application.Exceptions;

public class ValidationErrorsException : ApplicationErrorException
{
    public ValidationErrorsException(ValidationResult validationResult) : base(new Dictionary<string, string>(),
        HttpStatusCode.BadRequest)
    {
        foreach (var error in validationResult.Errors) Errors.TryAdd(error.PropertyName, error.ErrorMessage);
    }
}