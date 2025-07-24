namespace VibeMoment.BusinessLogic.DTOs.Photo;

public class UploadPhotoDto
{
    public string Title { get; set; }
    public byte[] Data { get; set; }
    public string? FileName { get; set; }
    public Guid UserId { get; set; }
}



