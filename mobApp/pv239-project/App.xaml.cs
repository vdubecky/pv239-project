using pv239_project.Services.Interfaces;
using pv239_project.ViewModels;

namespace pv239_project;

public partial class App : Application
{
    public static bool IsInForeground { get; private set; }
    
    public AuthViewModel AuthViewModel { get; }

    public App(AuthViewModel viewModel)
    {
        InitializeComponent();

        AuthViewModel = viewModel;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell(AuthViewModel));
    }

    protected async override void OnStart()
    {
        base.OnStart();
        IsInForeground = true;
    
#if ANDROID
        await Permissions.RequestAsync<NotificationPermission>();
#endif
    }

    protected override void OnSleep()
    {
        base.OnSleep();
        IsInForeground = false;
    }

    protected override void OnResume()
    {
        base.OnResume();
        IsInForeground = true;
    }
}
