namespace VibeMoment.BusinessLogic.DTOs.Photo;

public record UploadPhotoDto
{
    public string Title { get; init; }
    public byte[] Data { get; init; }
    public string? FileName { get; init; }
}


