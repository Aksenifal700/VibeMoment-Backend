namespace VibeMoment.BusinessLogic.DTOs.Photo;

public class UpdatePhotoDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? FileName { get; set; }
    public DateTime AddedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }
}
