using _6_WebSocket.Models;

namespace _6_WebSocket.SignalR;

using Microsoft.AspNetCore.SignalR;

public class TicketHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has just joined.");
    }
    
    // Chatting
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}");
    }
}
