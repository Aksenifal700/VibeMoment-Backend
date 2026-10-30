using VibeMoment.BusinessLogic.DTOs.Common;
using VibeMoment.BusinessLogic.Enums;

namespace VibeMoment.BusinessLogic.DTOs.Photo;

public class PhotosQueryDto : QueryParameters<PhotoSortBy>
{
    public Guid UserId { get; set; }
    public OrderDirection OrderBy { get; set; } = OrderDirection.Desc;
}