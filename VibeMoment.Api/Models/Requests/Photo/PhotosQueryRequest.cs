using VibeMoment.BusinessLogic.DTOs.Common;
using VibeMoment.BusinessLogic.Enums;

namespace VibeMoment.Api.Models.Requests.Photo;

public class PhotosQueryRequest : QueryParameters<PhotoSortBy>
{
    public Guid UserId { get; set; }
    public OrderDirection OrderBy { get; set; } = OrderDirection.Desc;
}