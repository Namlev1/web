namespace _6_WebSocket.Models;

public class Comment
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string Message { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}