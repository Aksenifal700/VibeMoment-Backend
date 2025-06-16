namespace VibeMoment.Api.Responses;

public class UpdatePhotoResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string ErrorMessage { get; set; } 
    public PhotoResponse? Photo { get; set; }
}