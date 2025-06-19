namespace VibeMoment.Api.Requests.Photo;

public class UploadPhotoRequest
{
    public string Title { get; set; }

    public IFormFile Photo { get; set; }
}