namespace VibeMoment.Api.Models.Responses;

public class PhotoResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public byte[] Data { get; set; }
    public DateTime AddedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
    
