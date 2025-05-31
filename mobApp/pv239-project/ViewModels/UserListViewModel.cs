using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Client;
using pv239_project.Mappers;
using pv239_project.Models;
using pv239_project.Services;
using pv239_project.Services.Interfaces;

namespace pv239_project.ViewModels;

public partial class UserListViewModel(IUserClient userClient, IConversationsService conversationsService, IUserService userService) : ObservableObject
{
    [ObservableProperty] public partial ICollection<User>? Items { get; set; }

    
    public async Task LoadDataAsync()
    {
        try
        {
            var items = await userClient.User_GetAllUsersAsync();
            Items = items
                .Where(s => s.Id != userService.CurrentUserId)
                .Select(s => s.UserDtoToUser()).ToObservableCollection();
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error loading users: {e.Message}");
        }
    }
    
    [RelayCommand]
    private async Task OpenConversation(User user)
    {
        conversationsService.SelectedConversation = new ConversationPreview
        {
            ConversationId = -1,
            LastMessage = "",
            Title = $"{user.Firstname} {user.Surname}",
        };
        
        await Shell.Current.GoToAsync(RoutingService.ConversationDetailPage,
            new Dictionary<string, object>
            {
                [nameof(ConversationDetailViewModel.ReceiverId)] = user.Id,
            });
    }
}
