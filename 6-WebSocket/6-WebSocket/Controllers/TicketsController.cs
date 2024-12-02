using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _6_WebSocket.Models;
using _6_WebSocket.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace _6_WebSocket.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketsController : ControllerBase
{
    public static readonly List<Ticket> Tickets = new();
    private readonly IHubContext<TicketHub> _hubContext;

    public TicketsController(IHubContext<TicketHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpGet]
    public IActionResult GetAllTickets()
    {
        return Ok(Tickets);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket)
    {
        ticket.Id = Tickets.Count + 1;
        Tickets.Add(ticket);
        await _hubContext.Clients.All.SendAsync("ReceiveNewTicket", ticket);
        return CreatedAtAction(nameof(GetAllTickets), new { id = ticket.Id }, ticket);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus)
    {
        var ticket = Tickets.FirstOrDefault(t => t.Id == id);
        if (ticket == null)
            return NotFound();

        ticket.Status = newStatus;
        ticket.UpdatedAt = DateTime.UtcNow;

        await _hubContext.Clients.All.SendAsync("ReceiveStatusChange", id, newStatus);
        return NoContent();
    }
}
