namespace pv239_project.Services.Interfaces;

public interface INotificationManagerService
{
    void SendNotification(string title, string message);
}