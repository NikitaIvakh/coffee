using System.Reflection;
using FluentValidation;
using Identity.Application.Behaviors;
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
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });
    }
}