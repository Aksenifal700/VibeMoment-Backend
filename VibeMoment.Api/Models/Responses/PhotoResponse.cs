namespace VibeMoment.Api.Models.Responses;

public class PhotoResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public byte[] Data { get; set; }
    public DateTime AddedAt { get; set; }
    public string ImageData { get; set; }
}
    
