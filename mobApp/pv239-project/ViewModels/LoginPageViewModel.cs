using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using pv239_project.Messages;
using pv239_project.Services;
using pv239_project.Services.Interfaces;

namespace pv239_project.ViewModels;

public partial class LoginPageViewModel(IUserService userService, IMessenger messenger) : ObservableObject
{
    [ObservableProperty] public partial string Email { get; set; } = string.Empty;

    [ObservableProperty] public partial string Password { get; set; } = string.Empty;

    [RelayCommand]
    private async Task SubmitAsync()
    {
        try
        {
            await userService.Login(Email, Password);
            messenger.Send(new AuthChangedMessage { IsAuthenticated = true });
        }
        catch (Exception e)
        {
            await Toast.Make(e.Message).Show();
        }
    }
    
    [RelayCommand]
    private async Task GoToCreateUser()
    {
        await Shell.Current.GoToAsync(RoutingService.CreateNewUserPage);
    }
}
