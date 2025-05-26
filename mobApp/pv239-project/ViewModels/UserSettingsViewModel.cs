using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Client;
using pv239_project.Mappers;

namespace pv239_project.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class UserSettingsViewModel : ObservableObject
{
    private readonly IPopupService _popupService;
    private readonly IUserClient _userClient;
    public int Id { get; init; } = 1;

    [ObservableProperty] public partial UserDto? User { get; set; }

    public UserSettingsViewModel(IPopupService popupService, IUserClient userClient)
    {
        _popupService = popupService;
        _userClient = userClient;
    }

    public async Task LoadDataAsync()
    {
        try
        {
            User = await _userClient.User_GetUserAsync(Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            // TODO: Handle
            // throw;
        }
    }

    [RelayCommand]
    private async Task UpdateUserSettings()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(User?.Firstname) ||
                string.IsNullOrWhiteSpace(User?.Surname) ||
                string.IsNullOrWhiteSpace(User?.Email))
            {
                return;
            }

            var updateUserDto = User!.ToUpdateUserDto();
            await _userClient.User_UpdateUserProfileAsync(Id, updateUserDto);

            var toast = Toast.Make("Successfully updated profile settings.");
            await toast.Show();
        }
        catch (Exception e)
        {
            var toast = Toast.Make(e.Message);
            await toast.Show();
        }
    }

    [RelayCommand]
    private async Task OpenChangePasswordPopup()
    {
        await _popupService.ShowPopupAsync<ChangePasswordPopupViewModel>();
    }
    
    [RelayCommand]
    private async Task OpenUploadProfilePicturePopup()
    {
        await _popupService.ShowPopupAsync<UploadProfilePicturePopupViewModel>();
    }
}
