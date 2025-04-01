using Microsoft.Extensions.Logging;
using pv239_project.Services;
using pv239_project.Services.Interfaces;

namespace pv239_project;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IRoutingService, RoutingService>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        var app = builder.Build();
        RegisterRoutes(app);
        
        return app;
    }
    
    private static void RegisterRoutes(MauiApp app)
    {
        var routingService = app.Services.GetRequiredService<IRoutingService>();

        foreach (var routeModel in routingService.Routes)
        {
            Routing.RegisterRoute(routeModel.Route, routeModel.ViewType);
        }
    }
}
