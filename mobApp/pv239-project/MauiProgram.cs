using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using pv239_project.Popups;
using pv239_project.Services;
using pv239_project.Services.Interfaces;
using pv239_project.ViewModels;

namespace pv239_project;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        RegisterServices(builder.Services);
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

    private static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Services
        services.AddSingleton<IRoutingService, RoutingService>();
        services.AddTransient<IUserService, UserService>();
        
        // View models
        services.AddTransient<UserListViewModel>();
        services.AddTransient<UserSettingsViewModel>();
        
        // Add popups
        services.AddTransientPopup<ChangePasswordPopup, ChangePasswordPopupViewModel>();

        return services;
    }
}
