using _6_WebSocket.Models;
using _6_WebSocket.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace _6_WebSocket.Controllers;

[ApiController]
[Route("api/tickets/{ticketId}/comments")]
public class CommentsController : ControllerBase
{
    private readonly IHubContext<TicketHub> _hubContext;

    public CommentsController(IHubContext<TicketHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(int ticketId, [FromBody] Comment comment)
    {
        var ticket = TicketsController.Tickets.FirstOrDefault(t => t.Id == ticketId);
        if (ticket == null)
            return NotFound();

        comment.Id = ticket.Comments.Count + 1;
        ticket.Comments.Add(comment);
        await _hubContext.Clients.All.SendAsync("ReceiveNewComment", ticketId, ticket.Comments);

        return CreatedAtAction(nameof(AddComment), new { id = comment.Id }, comment);
    }
}
