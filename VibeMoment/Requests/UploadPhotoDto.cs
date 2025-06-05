namespace VibeMoment.Requests;

public class UploadPhotoDto
{
    public string Title { get; set; }
    public IFormFile Photo { get; set; }
}