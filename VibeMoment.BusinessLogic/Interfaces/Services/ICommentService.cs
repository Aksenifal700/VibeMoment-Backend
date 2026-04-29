using VibeMoment.BusinessLogic.DTOs.Photo;

namespace VibeMoment.BusinessLogic.Interfaces.Services;

public interface ICommentService
{
    Task<CommentDto> AddCommentAsync(AddCommentDto dto);
    Task<List<CommentDto>> GetCommentByPhotoIdAsync(Guid photoId);
    Task DeleteCommentAsync(Guid commentId, Guid requestingUserId);
}