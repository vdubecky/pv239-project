using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using pv239_project.Client;
using pv239_project.Helpers;
using pv239_project.Mappers;
using pv239_project.Messages;

namespace pv239_project.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class UserSettingsViewModel : ObservableObject
{
    private readonly IPopupService _popupService;
    private readonly IUserClient _userClient;
    private readonly IMessenger _messenger;
    public int Id { get; init; }

    [ObservableProperty] public partial UserDto? User { get; set; }

    public UserSettingsViewModel(IPopupService popupService, IUserClient userClient, IMessenger messenger)
    {
        _popupService = popupService;
        _userClient = userClient;
        _messenger = messenger;

        Id = JwtHelpers.GetUserId();
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
    private async Task DeleteUser()
    {
        try
        {
            await _userClient.User_DeleteUserAsync(Id);

            SecureStorage.Remove("jwt_token");
            _messenger.Send(new AuthChangedMessage { IsAuthenticated = false });
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
        await _popupService.ShowPopupAsync<ChangePasswordPopupViewModel>(
            onPresenting: viewModel => viewModel.Id = Id
        );
    }

    [RelayCommand]
    private async Task OpenUploadProfilePicturePopup()
    {
        await _popupService.ShowPopupAsync<UploadProfilePicturePopupViewModel>(onPresenting: viewModel =>
            viewModel.Id = Id
        );
    }
}
