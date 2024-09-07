using System.Reflection;
using Coffee.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Coffee.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void ConfigureApplicationService(this IServiceCollection services)
    {
        RegisterInits(services);
    }

    private static void RegisterInits(IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });
    }
}