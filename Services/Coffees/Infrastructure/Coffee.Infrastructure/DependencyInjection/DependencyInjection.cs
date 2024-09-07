using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Providers;
using Coffee.Infrastructure.Interceptors;
using Coffee.Infrastructure.Providers;
using Coffee.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Coffee.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterRepositories(services);
        RegisterProviders(services);
        RegisterInterceptors(services);
        RegisterDataStorages(services, configuration);
        RegisterDatabase(services, configuration);
        ApplyMigration(services);
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICoffeeRepository, CoffeeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void RegisterProviders(IServiceCollection services)
    {
        services.AddSingleton<ICacheProvider, CacheProvider>();
    }

    private static void RegisterInterceptors(IServiceCollection services)
    {
        services.AddScoped<CacheInvalidationInterceptor>();
    }

    private static void RegisterDataStorages(IServiceCollection services, IConfiguration configuration)
    {
        const string connectionString = "RedisConnection";
        services.AddSingleton<IConnectionMultiplexer>(options => ConnectionMultiplexer.Connect(configuration.GetConnectionString(connectionString)!));
    }

    private static void RegisterDatabase(IServiceCollection services, IConfiguration configuration)
    {
        const string connectionString = "DefaultConnection";

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var cacheInvalidationInterceptor = sp.GetService<CacheInvalidationInterceptor>();
            options.UseNpgsql(configuration.GetConnectionString(connectionString)).AddInterceptors(cacheInvalidationInterceptor!);
        });
    }

    private static void ApplyMigration(IServiceCollection services)
    {
        var scope = services.BuildServiceProvider();
        using var serviceProvider = scope.CreateScope();
        var db = serviceProvider.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (db.Database.GetPendingMigrations().Any())
            db.Database.Migrate();
    }
}