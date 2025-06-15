namespace VibeMoment.Api.Requests;

public class UploadPhotoRequest
{
    public string Title { get; set; }
    public IFormFile Photo { get; set; }
}