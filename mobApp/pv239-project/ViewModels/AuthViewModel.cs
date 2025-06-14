using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.IdentityModel.JsonWebTokens;
using pv239_project.Messages;
using pv239_project.Services.Interfaces;

namespace pv239_project.ViewModels;

public partial class AuthViewModel : ObservableObject, IRecipient<AuthChangedMessage>
{
    private readonly IConversationsService _conversationsService;
    private readonly IUserService _userService;
    private readonly IHubService _hubService;
    
    
    [ObservableProperty] public partial bool IsAuthenticated { get; set; } = false;

    public AuthViewModel(IMessenger messenger, IHubService hubService, IConversationsService conversationsService, IUserService userService)
    {
        _conversationsService = conversationsService;
        _userService = userService;
        _hubService = hubService;
            
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
            InitAppServices();
        }
        else
        {
            SecureStorage.Remove("jwt_token");
        }
    }

    public async void Receive(AuthChangedMessage message)
    {
        if(!IsAuthenticated && message.IsAuthenticated)
        {
            await InitAppServices();
            IsAuthenticated = message.IsAuthenticated;
            await Shell.Current.GoToAsync("//ConversationListPage");
        }
        
        IsAuthenticated = message.IsAuthenticated;
        
        if (!message.IsAuthenticated)
        {
            await ClearAppServices();
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }

    private async Task ClearAppServices()
    {
        await _hubService.Clear();
        _conversationsService.Clear();
    }
    
    private async Task InitAppServices()
    {
        await _userService.Init();
        await _hubService.Start();
        await _conversationsService.Init();
    }
}
