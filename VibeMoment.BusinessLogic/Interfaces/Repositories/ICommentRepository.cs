using VibeMoment.BusinessLogic.DTOs.Photo;

namespace VibeMoment.BusinessLogic.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<CommentDto> AddAsync(AddCommentDto dto);
    Task<List<CommentDto>> GetByPhotoIdAsync(Guid photoId);
    Task<CommentDto?> GetByIdAsync(Guid commentId);
    Task<bool> DeleteAsync(Guid commentId);
}