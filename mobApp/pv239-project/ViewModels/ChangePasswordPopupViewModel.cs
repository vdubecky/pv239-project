using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Models;
using pv239_project.Services.Interfaces;

namespace pv239_project.ViewModels;

public partial class ChangePasswordPopupViewModel : ObservableObject
{
    private readonly IUserService _userService;
    private readonly IPopupService _popupService;

    public ChangePasswordPopupViewModel(IPopupService popupService, IUserService userService)
    {
        _popupService = popupService;
        _userService = userService;
    }
    
    [ObservableProperty]
    public partial string OldPassword { get; set; } = string.Empty;
    
    [ObservableProperty]
    public partial string NewPassword { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string ConfirmPassword { get; set; } = string.Empty;
    
    [RelayCommand]
    private async Task SubmitAsync()
    {
        if (OldPassword.Length < 6)
        {
            // TODO: Correctly handle
            return;
        }
        if (NewPassword.Length < 6)
        {
            return;
        }
        if (ConfirmPassword.Length < 6)
        {
            return;
        }
        if (NewPassword != ConfirmPassword)
        {
            await Shell.Current.DisplayAlert("Error", "Passwords do not match", "OK");
            return;
        }

        try
        {
            ChangePasswordDto changePasswordDto = new()
            {
                OldPassword = OldPassword,
                NewPassword = NewPassword,
                NewPasswordConfirm = ConfirmPassword,
            };
            await _userService.ChangePassword(1, changePasswordDto);
            await Toast.Make("Successfully updated password.").Show();
        }
        catch (Exception e)
        {
            await Toast.Make(e.Message).Show();
        }
        finally
        {
            await _popupService.ClosePopupAsync();
        }        
    }

    [RelayCommand]
    private async Task CloseAsync()
    {
        await _popupService.ClosePopupAsync();
    }
}
