namespace VibeMoment.Api.Requests;

public class SavePhotoRequest
{
    public string Title { get; set; } 
    public IFormFile Photo { get; set; }
}   