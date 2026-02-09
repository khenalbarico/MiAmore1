using LogicLbrary1.UserInterface1;
using Microsoft.Extensions.DependencyInjection;

namespace LogicLbrary1;

public static class ServiceRegistry
{
    public static void RegisterSvc(this IServiceCollection svc)
    {
        svc.AddScoped<CoreTabState>();
    }
}