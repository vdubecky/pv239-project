using pv239_project.Client;


namespace pv239_project.Services.Interfaces;

public interface IHubService
{
    Dictionary<string, Action<CreateMessageDto>> MessageHandler { get; set; }
    Task Start();
    Task Clear();
}