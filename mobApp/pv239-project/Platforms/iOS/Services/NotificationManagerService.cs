using pv239_project.Services.Interfaces;
using UserNotifications;

namespace pv239_project.Services;

/// <summary>
/// https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/local-notifications?view=net-maui-9.0&pivots=devices-ios
/// </summary>
public class NotificationManagerService : INotificationManagerService
{
    private int _messageId = 0;
    private bool _hasNotificationsPermission;
    
    public NotificationManagerService()
    {
        UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) =>
        {
            _hasNotificationsPermission = approved;
        });
    }

    public void SendNotification(string title, string message)
    {
        if (!_hasNotificationsPermission)
        {
            return;
        }
        
        _messageId++;
        var content = new UNMutableNotificationContent()
        {
            Title = title,
            Subtitle = "",
            Body = message,
            Badge = 1
        };

        var request = UNNotificationRequest.FromIdentifier(_messageId.ToString(), content, null);
        UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
        {
            if (err != null)
                throw new Exception($"Failed to schedule notification: {err}");
        });
    }
}

