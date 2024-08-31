using Coffee.Domain.Interfaces;
using Coffee.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coffee.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterRepositories(services);
        RegisterDatabase(services, configuration);
        ApplyMigration(services);
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICoffeeRepository, CoffeeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void RegisterDatabase(IServiceCollection services, IConfiguration configuration)
    {
        const string connectionString = "DefaultConnection";
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(connectionString));
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