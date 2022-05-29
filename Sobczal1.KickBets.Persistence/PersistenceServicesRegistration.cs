using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.Contracts.Persistence.Bet;
using Sobczal1.KickBets.Application.Contracts.Persistence.Football;
using Sobczal1.KickBets.Domain;
using Sobczal1.KickBets.Domain.Identity;
using Sobczal1.KickBets.Persistence.Repositories;
using Sobczal1.KickBets.Persistence.Repositories.Bets;
using Sobczal1.KickBets.Persistence.Repositories.Football;

namespace Sobczal1.KickBets.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<KickBetsDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("KickBetsConnectionString"));
            opt.EnableSensitiveDataLogging();
        });

        services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 5;
                opt.Password.RequiredUniqueChars = 1;
            })
            .AddEntityFrameworkStores<KickBetsDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();



        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBetsDataRepository, BetsDataRepository>();
        services.AddScoped<IBetRepository, BetRepository>();
        services.AddScoped<IWdlhtBetRepository, WdlhtBetRepository>();
        services.AddScoped<IWdlftBetRepository, WdlftBetRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IFixtureRepository, FixtureRepository>();
        services.AddScoped<ILeagueRepository, LeagueRepository>();
        services.AddScoped<ILineupRepository, LineupRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IScoreRepository, ScoreRepository>();
        services.AddScoped<IStatisticsRepository, StatisticRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IVenueRepository, VenueRepository>();

        services.AddScoped<IDbUpdateRepository, DbUpdateRepository>();
        
        return services;
    }

    public static async Task PersistenceSeedUsers(this IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<KickBetsDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            await SeedUsers(context, userManager);
        }
        catch (Exception e)
        {
            var logger = services.GetRequiredService<ILogger<KickBetsDbContext>>();
            logger.LogError(e, "An error occured while seeding users");
        }
    }

    private static async Task SeedUsers(KickBetsDbContext context, UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<AppUser>
            {
                new AppUser {UserName = "Kasia", Email = "kasia@test.com", Balance = 1e6, BalanceLastAddedAt = DateTime.Now.AddDays(-1)},
                new AppUser {UserName = "Lukasz", Email = "lukasz@test.com", Balance = 1e6, BalanceLastAddedAt = DateTime.Now.AddDays(-1)},
                new AppUser {UserName = "Tester", Email = "tester@test.com", Balance = 1e6, BalanceLastAddedAt = DateTime.Now.AddDays(-1)},
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "password");
            }
        }
    }
}