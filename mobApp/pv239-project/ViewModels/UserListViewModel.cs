using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Client;
using pv239_project.Mappers;
using pv239_project.Models;
using pv239_project.Services;

namespace pv239_project.ViewModels;

public partial class UserListViewModel(IUserClient userClient) : ObservableObject
{
    [ObservableProperty] public partial ICollection<User>? Items { get; set; }

    
    public async Task LoadDataAsync()
    {
        try
        {
            var items = await userClient.User_GetAllUsersAsync();
            Items = items.Select(s => s.UserDtoToUser()).ToObservableCollection();
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error loading users: {e.Message}");
        }
    }
    
    [RelayCommand]
    private async Task OpenConversation(User user)
    {
        await Shell.Current.GoToAsync(RoutingService.ConversationDetailPage,
            new Dictionary<string, object>
            {
                [nameof(ConversationDetailViewModel.ConversationName)] = $"{user.Firstname} {user.Surname}",
                [nameof(ConversationDetailViewModel.ReceiverId)] = user.Id,
            });
    }
}
