using System.Net;
using Newtonsoft.Json;
using Sobczal1.KickBets.Application.Exceptions;

namespace Sobczal1.KickBets.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exeption)
        {
            await HandleExceptionAsync(httpContext, exeption);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var statusCode = HttpStatusCode.InternalServerError;
        var result = JsonConvert.SerializeObject(new {Errors = new {Failure = exception.Message}});
        if (exception is ApplicationErrorException errorException)
        {
            statusCode = errorException.Code;
            result = JsonConvert.SerializeObject(new {errorException.Errors});
        }

        context.Response.StatusCode = (int) statusCode;
        return context.Response.WriteAsync(result);
    }
}