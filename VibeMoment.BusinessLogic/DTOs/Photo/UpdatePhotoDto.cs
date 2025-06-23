namespace VibeMoment.BusinessLogic.DTOs.Photo;

public record UpdatePhotoDto
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string? FileName { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public DateTime AddedAt { get; init; } = DateTime.UtcNow;
}
