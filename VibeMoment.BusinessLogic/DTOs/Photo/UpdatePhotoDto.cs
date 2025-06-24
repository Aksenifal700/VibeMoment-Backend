namespace VibeMoment.BusinessLogic.DTOs.Photo;

public class UpdatePhotoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? FileName { get; set; }
    public DateTime AddedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }
}
