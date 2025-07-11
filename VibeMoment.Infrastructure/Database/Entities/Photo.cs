using Microsoft.AspNetCore.Identity;

namespace VibeMoment.Infrastructure.Database.Entities;

public class Photo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public byte[] Data { get; set; } = [];

    public DateTime AddedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }
    
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
    
}