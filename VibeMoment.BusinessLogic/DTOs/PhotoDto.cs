namespace VibeMoment.BusinessLogic.DTOs;

public class PhotoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public byte[]? Data { get; set; }
    public DateTime AddedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
}
