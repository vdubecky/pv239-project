using System.Reflection;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using pv239_project.Client;
using pv239_project.Configuration;
using pv239_project.Middleware;
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

        ConfigureAppSettings(builder);
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

    private static void ConfigureAppSettings(MauiAppBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder();

        var assembly = Assembly.GetExecutingAssembly();
        var appSettingsFilePath = "pv239_project.Configuration.appsettings.json";
        using var appSettingsStream = assembly.GetManifestResourceStream(appSettingsFilePath);
        if (appSettingsStream is not null)
        {
            configurationBuilder.AddJsonStream(appSettingsStream);
        }

        var configuration = configurationBuilder.Build();
        builder.Configuration.AddConfiguration(configuration);

        builder.Services.Configure<ApiOptions>(options =>
            builder.Configuration.GetSection(nameof(ApiOptions)).Bind(options)
        );
    }

    private static IServiceCollection RegisterServices(IServiceCollection services)
    {
        // Services
        services.AddSingleton<IRoutingService, RoutingService>();

        // View models
        services.AddTransient<UserListViewModel>();
        services.AddTransient<UserSettingsViewModel>();
        services.AddTransient<LoginPageViewModel>();
        services.AddTransient<CreateNewUserViewModel>();
        services.AddTransient<AuthViewModel>();

        // Add popups
        services.AddTransientPopup<ChangePasswordPopup, ChangePasswordPopupViewModel>();
        services.AddTransientPopup<UploadProfilePicturePopup, UploadProfilePicturePopupViewModel>();

        // Http Client
        services.AddTransient<AuthHandler>();


        services.AddHttpClient<IUserClient, UserClient>((serviceProvider, client) =>
        {
            var apiOptions = serviceProvider.GetRequiredService<IOptions<ApiOptions>>();
            client.BaseAddress = new Uri(apiOptions.Value.ApiUrl);
        }).AddHttpMessageHandler<AuthHandler>();
        services.AddHttpClient<IAuthenticationClient, AuthenticationClient>((serviceProvider, client) =>
        {
            var apiOptions = serviceProvider.GetRequiredService<IOptions<ApiOptions>>();
            client.BaseAddress = new Uri(apiOptions.Value.ApiUrl);
        });

        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

        return services;
    }
}
