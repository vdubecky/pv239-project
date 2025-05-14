using Microsoft.AspNetCore.SignalR;
using System.Runtime.InteropServices;

namespace ChatAppBackend.Hubs
{
    public class ChatAppHub : Hub
    {       
        public static Dictionary<string, string> Users = new();

        public override Task OnConnectedAsync()
        {
            string userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();
            string clientId = Context.ConnectionId;

            if(Users.ContainsKey(userId))
            {
                Users.Remove(userId);
            }

            Users.Add(userId, clientId);
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {                       
            return base.OnDisconnectedAsync(exception);
        }
    }
}
