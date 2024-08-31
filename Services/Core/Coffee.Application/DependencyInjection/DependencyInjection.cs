using System.Reflection;
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
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }
}