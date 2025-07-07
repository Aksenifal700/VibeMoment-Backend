namespace VibeMoment.BusinessLogic.DTOs.Photo;

public class PhotosQuery
{
    public string UserId { get; set; }
    public string? OrderBy { get; set; } = "desc";
}