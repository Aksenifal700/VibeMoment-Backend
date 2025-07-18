using VibeMoment.BusinessLogic.Enums;

namespace VibeMoment.BusinessLogic.DTOs.Photo;

public class PhotosQueryDto
{
    public Guid UserId { get; set; }
    public OrderDirection OrderBy { get; set; } = OrderDirection.Desc;
}