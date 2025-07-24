using VibeMoment.BusinessLogic.Enums;

namespace VibeMoment.Api.Models.Requests.Photo;

public class PhotosQueryRequest
{
    public Guid UserId { get; set; }
    public OrderDirection OrderBy { get; set; } = OrderDirection.Desc;
}