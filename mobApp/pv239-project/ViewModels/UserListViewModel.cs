using CommunityToolkit.Mvvm.ComponentModel;
using pv239_project.Models;
using pv239_project.Services.Interfaces;

namespace pv239_project.ViewModels;

public partial class UserListViewModel : ObservableObject
{
    private readonly IUserService _userService;
    
    [ObservableProperty]
    public partial ICollection<UserDto>? Items { get; set; }
    
    public UserListViewModel(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task LoadDataAsync()
    {
        Items = await _userService.GetAllUsers();
    }
}
