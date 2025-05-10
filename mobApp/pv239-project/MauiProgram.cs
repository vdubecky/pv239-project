using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using pv239_project.Client;
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
        
        // View models
        services.AddTransient<UserListViewModel>();
        services.AddTransient<UserSettingsViewModel>();
        services.AddTransient<LoginPageViewModel>();
        services.AddTransient<AuthViewModel>();
        
        // Add popups
        services.AddTransientPopup<ChangePasswordPopup, ChangePasswordPopupViewModel>();
        services.AddTransientPopup<UploadProfilePicturePopup, UploadProfilePicturePopupViewModel>();

        // Client
        services.AddHttpClient<IUserClient, UserClient>(client =>
        {
            client.BaseAddress = new Uri("http://192.168.0.154:5115/");
        });
        services.AddHttpClient<IAuthenticationClient, AuthenticationClient>(client =>
        {
            client.BaseAddress = new Uri("http://192.168.0.154:5115/");
        });
        
        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

        return services;
    }
}
