namespace VibeMoment.Infrastructure.Database.Entities;

public class Photo
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public byte[] Data { get; set; } = [];

    public DateTime AddedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}