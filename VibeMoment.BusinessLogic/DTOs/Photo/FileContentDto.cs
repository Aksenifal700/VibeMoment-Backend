namespace VibeMoment.BusinessLogic.DTOs.Photo;

public class FileContentDto
{
    public required Stream Content { get; init; }
    public required string ContentType { get; init; }
}