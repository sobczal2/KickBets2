using System.Net;

namespace Sobczal1.KickBets.Application.Exceptions;

public class ApplicationErrorException : ApplicationException
{
    public Dictionary<string, string> Errors { get; set; }
    public HttpStatusCode Code { get; set; }
    public ApplicationErrorException(Dictionary<string, string> errors, HttpStatusCode code)
    {
        Errors = errors;
        Code = code;
    }
}