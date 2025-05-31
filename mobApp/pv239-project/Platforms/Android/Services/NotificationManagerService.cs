using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using pv239_project.Services.Interfaces;

namespace pv239_project.Services;


/// <summary>
/// https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/local-notifications?view=net-maui-9.0&pivots=devices-android
/// </summary>
public class NotificationManagerService : INotificationManagerService 
{
    public const string ChannelId = "default";
    public const string ChannelName = "Default";
    public const string ChannelDescription = "The default channel for notifications.";

    public const string TitleKey = "title";
    public const string MessageKey = "message";
    
    private readonly NotificationManagerCompat _compatManager;
    
    private int _messageId = 0;
    private int _pendingIntentId = 0;

    public static NotificationManagerService? Instance { get; private set; }

    
    public NotificationManagerService()
    {
        if (Instance != null)
        {
            return;
        }
        
        CreateNotificationChannel();
        _compatManager = NotificationManagerCompat.From(Platform.AppContext);
        Instance = this;
    }

    private void CreateNotificationChannel()
    {
        if (Build.VERSION.SdkInt < BuildVersionCodes.O)
        {
            return;
        }
        
        var channelNameJava = new Java.Lang.String(ChannelName);
        var channel = new NotificationChannel(ChannelId, channelNameJava, NotificationImportance.Default);
        channel.Description = ChannelDescription;

        NotificationManager manager = (NotificationManager)Platform.AppContext.GetSystemService(Context.NotificationService);
        manager.CreateNotificationChannel(channel);
    }
    
    public void SendNotification(string title, string message)
    {
        Intent intent = new Intent(Platform.AppContext, typeof(MainActivity));
        intent.PutExtra(TitleKey, title);
        intent.PutExtra(MessageKey, message);
        intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

        var pendingIntentFlags = (Build.VERSION.SdkInt >= BuildVersionCodes.S)
            ? PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable
            : PendingIntentFlags.UpdateCurrent;

        PendingIntent pendingIntent = PendingIntent.GetActivity(Platform.AppContext, _pendingIntentId++, intent, pendingIntentFlags);
        NotificationCompat.Builder builder = new NotificationCompat.Builder(Platform.AppContext, ChannelId)
            .SetContentIntent(pendingIntent)
            .SetContentTitle(title)
            .SetContentText(message)
            .SetSmallIcon(Resource.Drawable.ic_keyboard_black_24dp);

        Notification notification = builder.Build();
        _compatManager.Notify(_messageId++, notification);  
    }
}