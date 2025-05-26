using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using pv239_project.Client;
using pv239_project.Messages;

namespace pv239_project.ViewModels;

public partial class CreateNewUserViewModel : ObservableObject
{
    private readonly IUserClient _userClient;
    private readonly IAuthenticationClient _authenticationClient;
    private readonly IMessenger _messenger;

    public CreateNewUserViewModel(IUserClient userClient, IAuthenticationClient authenticationClient, IMessenger messenger)
    {
        _userClient = userClient;
        _authenticationClient = authenticationClient;
        _messenger = messenger;
    }

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
            
            var updateUserDto = new UserRegisterDto()
            {
                Firstname = Firstname,
                Surname = Surname,
                Email = Email,
                Password = Password,
            };
            await _userClient.User_CreateAccountAsync(updateUserDto);

            UserLoginDto userLoginDto = new UserLoginDto()
            {
                Email = Email,
                Password = Password,
            };
            var token = await _authenticationClient.Authentication_LoginAsync(userLoginDto);

            await SecureStorage.SetAsync("jwt_token", token);
            
            _messenger.Send(new AuthChangedMessage { IsAuthenticated = true });
        }
        catch (Exception e)
        {
            var toast = Toast.Make(e.Message);
            await toast.Show();
        }
    }
}
