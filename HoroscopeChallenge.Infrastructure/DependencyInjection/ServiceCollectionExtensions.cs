using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HoroscopeChallenge.Infrastructure.Persistence;
using HoroscopeChallenge.Domain.Repositories;
using HoroscopeChallenge.Infrastructure.Repositories;
using HoroscopeChallenge.Domain.Interfaces;
using HoroscopeChallenge.Infrastructure.Repositories.Repositories;

namespace HoroscopeChallenge.Infrastructure.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
            );
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IHoroscopeQueryRepository, HoroscopeQueryRepository>();
        services.AddScoped<IHoroscopeCacheRepository, HoroscopeCacheRepository>();

        var baseUrl = configuration["HoroscopeApi:BaseUrl"];

        services.AddHttpClient<IHoroscopeService, HoroscopeService>(client =>
        {
            client.BaseAddress = new Uri(baseUrl!);
        });

        return services;
    }
}