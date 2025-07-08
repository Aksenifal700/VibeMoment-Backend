using VibeMoment.BusinessLogic.Enums;

namespace VibeMoment.BusinessLogic.DTOs.Photo;

public class PhotosQuery
{
    public string UserId { get; set; }
    public OrderDirection OrderBy { get; set; } = OrderDirection.Desc;
}