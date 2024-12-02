using _6_WebSocket.Controllers;

namespace _6_WebSocket.SignalR;

using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;
using _6_WebSocket.SignalR;
using _6_WebSocket.Models;

public class TaskBroadcastService : BackgroundService
{
    private readonly IHubContext<TicketHub> _hubContext;

    public TaskBroadcastService(IHubContext<TicketHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Get all tasks (modify this to fetch your task data)
            var tasks = TicketsController.Tickets;

            // Send tasks to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveAllTasks", tasks);

            // Wait for 5 seconds
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
