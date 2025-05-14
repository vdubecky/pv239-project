using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Client;
using pv239_project.Mappers;
using pv239_project.Models;
using pv239_project.Services;

namespace pv239_project.ViewModels;

public partial class UserListViewModel : ObservableObject
{
    private readonly IUserClient _userClient;

    [ObservableProperty] public partial ICollection<User>? Items { get; set; }

    public UserListViewModel(IUserClient userClient)
    {
        _userClient = userClient;
    }

    public async Task LoadDataAsync()
    {
        var items = await _userClient.User_GetAllUsersAsync();
        Items = items.Select(s => s.UserDtoToUser()).ToObservableCollection();
    }

    [RelayCommand]
    private async Task OpenConversation(int id)
    {
        await Shell.Current.GoToAsync(RoutingService.ConversationDetailPage,
            new Dictionary<string, object>
            {
                [nameof(ConversationDetailViewModel.ReceiverId)] = id,
            });
    }
}
