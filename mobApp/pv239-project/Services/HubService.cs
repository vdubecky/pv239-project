using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.SignalR.Client;
using pv239_project.Client;
using pv239_project.Services.Interfaces;


namespace pv239_project.Services
{
    public class HubService : IHubService
    {
        public required Dictionary<string, Action<CreateMessageDto>> MessageHandlers { get; set; } = new();

        private readonly HubConnection _connection;


        public HubService()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl($"{MauiProgram.BASE_URL}chatAppHub?userId=1")
                .WithAutomaticReconnect()
                .Build();

            Start();
        }

        public async Task Start()
        {
            await _connection.StartAsync();
            _connection.On<int, CreateMessageDto>("SendMessage", (conversationId, messageDto) =>
            {
                MessageHandlers.TryGetValue(conversationId.ToString(), out var handler);
                handler?.Invoke(messageDto);
            });
        }
    }
}
