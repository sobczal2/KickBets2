using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sobczal1.KickBets.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<KickBetsDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("KickBetsConnectionString")));
        return services;
    }
}