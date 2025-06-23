namespace VibeMoment.BusinessLogic.DTOs.Photo;

public record PhotoDto
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string UserId { get; init; }
    public byte[]? Data { get; init; }
    public DateTime AddedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}