using LogicLbrary1.MusicPlaylistHandler1;
using LogicLbrary1.SmtpHandler1;
using LogicLbrary1.UserInterface1;
using Microsoft.Extensions.DependencyInjection;

namespace LogicLbrary1;

public static class ServiceRegistry
{
    public static void RegisterSvc(this IServiceCollection svc)
    {
        svc.AddScoped<CoreTabState>();
        svc.AddScoped<SmtpGmail1>();
        svc.AddScoped<MusicLibraryService>();
        svc.AddScoped<MusicPlayerState>();
    }
}