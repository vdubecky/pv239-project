using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Client;

namespace pv239_project.ViewModels;

public partial class UploadProfilePicturePopupViewModel : ObservableObject
{
    private readonly IUserClient _userClient;
    private readonly IPopupService _popupService;

    [ObservableProperty] public partial byte[]? ImageBytes { get; set; }

    [ObservableProperty] public partial string? ImageType { get; set; }

    public UploadProfilePicturePopupViewModel(IUserClient userClient, IPopupService popupService)
    {
        _userClient = userClient;
        _popupService = popupService;
    }

    [RelayCommand]
    private async Task PickPicture()
    {
        if (!MediaPicker.Default.IsCaptureSupported)
        {
            return;
        }

        FileResult? photo = await MediaPicker.Default.PickPhotoAsync();
        if (photo is null)
        {
            return;
        }

        ImageType = photo.ContentType;
        var stream = await photo.OpenReadAsync();
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        ImageBytes = memoryStream.ToArray();
    }

    [RelayCommand]
    private async Task SubmitAsync()
    {
        if (ImageBytes is null || ImageType is null)
        {
            return;
        }

        try
        {
            var fileName = Guid.NewGuid().ToString();
            using var stream = new MemoryStream(ImageBytes);
            var file = new FileParameter(stream, fileName, ImageType);
            await _userClient.User_UploadUserPictureAsync(
                1,
                file,
                $"{fileName}.{MapContentTypeToExtension(ImageType)}"
            );
            await Toast.Make("Successfully uploaded profile picture.").Show();
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

    private string MapContentTypeToExtension(string contentType)
    {
        return contentType switch
        {
            "image/jpeg" => "jpg",
            "image/png" => "png",
            _ => contentType
        };
    }
}
