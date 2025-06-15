namespace VibeMoment.BusinessLogic.Requests;

public class UploadPhotoRequest
{
    public string Title { get; set; }
    public byte[] PhotoData { get; set; }
    public string FileName { get; set; }
}