using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using pv239_project.Client;
using pv239_project.Mappers;
using pv239_project.Models;

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
        try
        {
            var items = await _userClient.User_GetAllUsersAsync();
            Items = items.Select(s => s.UserDtoToUser()).ToObservableCollection();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            // TODO: Handle
            // throw;
        }
    }
}
