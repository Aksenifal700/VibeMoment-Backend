namespace VibeMoment.Infrastructure.Database.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public Guid UserId { get; set; }
    public Guid PhotoId { get; set; }
}