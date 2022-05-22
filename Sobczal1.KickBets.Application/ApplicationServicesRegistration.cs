using System.Globalization;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sobczal1.KickBets.Application.Services.ApiFootball;
using Sobczal1.KickBets.Application.Services.Identity;

namespace Sobczal1.KickBets.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<FootballApiService>();
        services.AddHostedService<DbUpdateHostedService>();

        services.AddScoped<TokenService>();

        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US");
        
        return services;
    }
}