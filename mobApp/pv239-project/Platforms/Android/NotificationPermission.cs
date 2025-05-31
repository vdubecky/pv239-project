namespace pv239_project;

/// <summary>
/// https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/local-notifications?view=net-maui-9.0&pivots=devices-android
/// </summary>
public class NotificationPermission : Permissions.BasePlatformPermission
{
    public override (string androidPermission, bool isRuntime)[] RequiredPermissions
    {
        get
        {
            var result = new List<(string androidPermission, bool isRuntime)>();
            if (OperatingSystem.IsAndroidVersionAtLeast(33))
                result.Add(("android.permission.POST_NOTIFICATIONS", true));
            return result.ToArray();
        }
    }
}