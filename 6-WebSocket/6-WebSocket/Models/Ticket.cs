namespace _6_WebSocket.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } = "Open"; // Default status
    public string Priority { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public List<Comment> Comments { get; set; } = new();
}