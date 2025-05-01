using Microsoft.AspNetCore.SignalR;

namespace ChatAppBackend.Hubs
{
    public class ChatAppHub : Hub
    {
        public async Task SendMessage(string conversationId, string message)
        {
            Clients.Client(conversationId).SendAsync("ReceiveMessage", message);
        }
    }
}
