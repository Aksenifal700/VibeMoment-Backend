namespace VibeMoment.Requests;

public class UploadPhotoDto
{
    public string Name { get; set; }
    public IFormFile File { get; set; }
}