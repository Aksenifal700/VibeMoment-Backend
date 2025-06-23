namespace VibeMoment.Api.Requests.Photo;

public record UploadPhotoRequest
{
       public string Title { get; init; }
       public IFormFile Photo { get; init; }
}

   
