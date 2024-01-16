namespace Shared;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Content { get; set; }
    public string? Sender { get; set; }
    public DateTimeOffset CreatedAt {  get; set; } 
}
