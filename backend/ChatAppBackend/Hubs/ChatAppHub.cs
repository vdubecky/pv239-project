using Microsoft.AspNetCore.SignalR;
using System.Runtime.InteropServices;

namespace ChatAppBackend.Hubs;

public class ChatAppHub : Hub
{       
    public static readonly Dictionary<string, string> Users = new();

    public override Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();

        if (userId == null)
        {
            return base.OnConnectedAsync();    
        }
            
        Users.Remove(userId);
        Users.Add(userId, Context.ConnectionId);
        return base.OnConnectedAsync();
    }
}