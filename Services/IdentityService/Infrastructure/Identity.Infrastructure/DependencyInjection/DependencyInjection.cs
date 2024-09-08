using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterDatabase(services, configuration);
        ApplyMigration(services);
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
        var serviceProvider = scope.CreateScope();
        using var db = serviceProvider.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (db.Database.GetPendingMigrations().Any())
            db.Database.Migrate();
    }
}