namespace VibeMoment.BusinessLogic.DTOs.Photo;

public class PhotoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string UserId { get; set; }
    public byte[]? Data { get; set; }
    public DateTime AddedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}