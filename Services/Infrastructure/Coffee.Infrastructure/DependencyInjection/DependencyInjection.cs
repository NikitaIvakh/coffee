using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Providers;
using Coffee.Infrastructure.Jobs;
using Coffee.Infrastructure.Options;
using Coffee.Infrastructure.Providers;
using Coffee.Infrastructure.Repositories;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace Coffee.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterStorages(services, configuration);
        RegisterRepositories(services);
        RegisterJobs(services);
        RegisterProviders(services);
        RegisterHangfire(services, configuration);
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
        services.AddScoped<IMinioProvider, MinioProvider>();
    }

    private static void RegisterJobs(IServiceCollection services)
    {
        services.AddScoped<IImageCleanUpJob, ImageCleanUpJob>();
    }

    private static void RegisterHangfire(IServiceCollection services, IConfiguration configuration)
    {
        const string connectionString = "DefaultConnection";

        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(c => c
                .UseNpgsqlConnection(configuration.GetConnectionString(connectionString))));

        services.AddHangfireServer(options => options.SchedulePollingInterval = TimeSpan.FromSeconds(5));
    }

    private static void RegisterStorages(IServiceCollection services, IConfiguration configuration)
    {
        var minioOptions = configuration.GetSection("Minio").Get<MinioOptions>();
            
        if (minioOptions == null)
            throw new Exception("Minio configuration not found");
        
        services.AddMinio(options =>
        {
            options.WithEndpoint(minioOptions.Endpoint)
                .WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey)
                .WithSSL(false); 
        });
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