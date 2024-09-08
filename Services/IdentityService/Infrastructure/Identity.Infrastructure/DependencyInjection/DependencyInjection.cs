using Identity.Application.Abstractors.Interfaces;
using Identity.Infrastructure.Interceptors;
using Identity.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterRepositories(services);
        RegisterInterceptors(services);
        RegisterDatabase(services, configuration);
        ApplyMigration(services);
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void RegisterInterceptors(IServiceCollection services)
    {
        services.AddScoped<RegisterUserInterceptor>();
    }

    private static void RegisterDatabase(IServiceCollection services, IConfiguration configuration)
    {
        const string connectionString = "DefaultConnection";

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var registerUserInterceptor = sp.GetRequiredService<RegisterUserInterceptor>();
            options.UseNpgsql(configuration.GetConnectionString(connectionString)).AddInterceptors(registerUserInterceptor);
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