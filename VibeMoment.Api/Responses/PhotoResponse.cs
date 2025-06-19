namespace VibeMoment.Api.Responses;

public class PhotoResponse
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string Title { get; set; }
    public DateTime AddedAt { get; set; }
    public string ImageData { get; set; }
}