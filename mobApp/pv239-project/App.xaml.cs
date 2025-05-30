using CommunityToolkit.Mvvm.Messaging;
using pv239_project.Services.Interfaces;
using pv239_project.ViewModels;

namespace pv239_project;

public partial class App : Application
{
    public AuthViewModel AuthViewModel { get; }
    
    public App(AuthViewModel viewModel, IHubService hubService, IConversationsService conversationsService)
    {
        InitializeComponent();
        
        AuthViewModel = viewModel;
        
        hubService.Start();
        conversationsService.Init();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell(AuthViewModel));
    }
}
