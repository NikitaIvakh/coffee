using System.Reflection;
using Identity.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        RegisterServices(services);
        RegisterInits(services);
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
    }

    private static void RegisterInits(IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}