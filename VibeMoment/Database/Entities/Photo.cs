using VibeMoment.Extension;

namespace VibeMoment.Database.Entities;

public class Photo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public byte[] Data { get; set; } = [];
    
    public DateTime AddedAt { get; set; }= DateTime.UtcNow;
    public bool CanEdit => AddedAt.CanEdit();
    public string TimeAgo => AddedAt.TimeAgo();
}