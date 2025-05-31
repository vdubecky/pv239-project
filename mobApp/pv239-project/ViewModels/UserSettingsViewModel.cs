using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using pv239_project.Client;
using pv239_project.Mappers;
using pv239_project.Messages;
using pv239_project.Models;
using pv239_project.Services;
using pv239_project.Services.Interfaces;

namespace pv239_project.ViewModels;


public partial class UserSettingsViewModel(IPopupService popupService, IMessenger messenger, IUserService userService) : ObservableObject
{
    [ObservableProperty] public partial User User { get; set; } = (User)userService.CurrentUser.Clone();

    
    [RelayCommand]
    private async Task UpdateUserSettings()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(User.Firstname) ||
                string.IsNullOrWhiteSpace(User.Surname) ||
                string.IsNullOrWhiteSpace(User.Email))
            {
                return;
            }

            await userService.UpdateUserSettings(User);

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
            await userService.DeleteUser();
            messenger.Send(new AuthChangedMessage { IsAuthenticated = false });
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
        await popupService.ShowPopupAsync<ChangePasswordPopupViewModel>(
            onPresenting: viewModel => viewModel.Id = User.Id
        );
    }

    [RelayCommand]
    private async Task OpenUploadProfilePicturePopup()
    {
        await popupService.ShowPopupAsync<UploadProfilePicturePopupViewModel>(onPresenting: viewModel =>
            viewModel.Id = User.Id
        );
    }

    [RelayCommand]
    private async Task LogOut()
    {
        await userService.LogOut();
        messenger.Send(new AuthChangedMessage { IsAuthenticated = false });
    }
}
