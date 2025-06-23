namespace VibeMoment.Api.Responses;

public record PhotoResponse
{
    public int Id { get; init; }
    public string Title { get; init; }
    public byte[] Data { get; init; }
    public DateTime AddedAt { get; init; }
    public string ImageData { get; init; }
}
    
