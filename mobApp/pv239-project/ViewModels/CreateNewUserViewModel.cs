using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using pv239_project.Messages;
using pv239_project.Services.Interfaces;

namespace pv239_project.ViewModels;

public partial class CreateNewUserViewModel(IUserService userService, IMessenger messenger) : ObservableObject
{
    [ObservableProperty] public partial string Firstname { get; set; } = string.Empty;
    [ObservableProperty] public partial string Surname { get; set; } = string.Empty;
    [ObservableProperty] public partial string Email { get; set; } = string.Empty;
    [ObservableProperty] public partial string Password { get; set; } = string.Empty;
    [ObservableProperty] public partial string ConfirmPassword { get; set; } = string.Empty;

    [RelayCommand]
    private async Task CreateNewUser()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Firstname) ||
                string.IsNullOrWhiteSpace(Surname) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                return;
            }
            
            if (Password.Length < 6)
            {
                return;
            }
            if (ConfirmPassword.Length < 6)
            {
                return;
            }
            if (Password != ConfirmPassword)
            {
                await Shell.Current.DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            await userService.Register(Firstname, Surname, Email, Password);
            messenger.Send(new AuthChangedMessage { IsAuthenticated = true });
        }
        catch (Exception e)
        {
            var toast = Toast.Make(e.Message);
            await toast.Show();
        }
    }
}
