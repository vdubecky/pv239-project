using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using pv239_project.Client;
using pv239_project.Messages;

namespace pv239_project.ViewModels;

public partial class LoginPageViewModel : ObservableObject
{
    private readonly IAuthenticationClient _authenticationClient;
    private readonly IMessenger _messenger;

    public LoginPageViewModel(IAuthenticationClient authenticationClient, IMessenger messenger)
    {
        _authenticationClient = authenticationClient;
        _messenger = messenger;
    }

    [ObservableProperty] public partial string Email { get; set; } = string.Empty;

    [ObservableProperty] public partial string Password { get; set; } = string.Empty;

    [RelayCommand]
    private async Task SubmitAsync()
    {
        try
        {
            UserLoginDto userLoginDto = new UserLoginDto()
            {
                Email = Email,
                Password = Password,
            };
            var token = await _authenticationClient.Authentication_LoginAsync(userLoginDto);
            await Toast.Make(token).Show();

            await SecureStorage.SetAsync("jwt_token", token);
            _messenger.Send(new AuthChangedMessage { IsAuthenticated = true });
        }
        catch (Exception e)
        {
            await Toast.Make(e.Message).Show();
        }
    }
}
