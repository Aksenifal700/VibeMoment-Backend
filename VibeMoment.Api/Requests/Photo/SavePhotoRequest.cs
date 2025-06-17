namespace VibeMoment.Api.Requests.Photo;

public class SavePhotoRequest
{
    public string Title { get; set; } 
    public IFormFile Photo { get; set; }
}   