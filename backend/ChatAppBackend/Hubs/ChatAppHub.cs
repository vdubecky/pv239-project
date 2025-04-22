using Microsoft.AspNetCore.SignalR;

namespace ChatAppBackend.Hubs
{
    public class ChatAppHub : Hub
    {
        public async Task SendMessage(string conversationId, string message)
        {
            // Najdeme konverzaci podle ID
            // a odešleme zprávu všem uživatelům v této konverzaci

            
        }
    }
}
