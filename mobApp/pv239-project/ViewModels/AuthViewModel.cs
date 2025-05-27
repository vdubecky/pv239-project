using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.IdentityModel.JsonWebTokens;
using pv239_project.Messages;

namespace pv239_project.ViewModels;

public partial class AuthViewModel : ObservableObject, IRecipient<AuthChangedMessage>
{
    [ObservableProperty] public partial bool IsAuthenticated { get; set; } = false;

    public AuthViewModel(IMessenger messenger)
    {
        messenger.RegisterAll(this);
        var token = SecureStorage.GetAsync("jwt_token").Result;
        if (token is null)
        {
            return;
        }

        var jwt = new JsonWebToken(token);
        if (jwt.ValidTo > DateTime.UtcNow)
        {
            IsAuthenticated = true;
        }
        else
        {
            SecureStorage.Remove("jwt_token");
        }
    }

    public void Receive(AuthChangedMessage message)
    {
        IsAuthenticated = message.IsAuthenticated;
    }
}
