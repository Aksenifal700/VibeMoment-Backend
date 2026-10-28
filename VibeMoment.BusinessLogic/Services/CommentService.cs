using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.BusinessLogic.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPhotoRepository _photoRepository;

    public CommentService(ICommentRepository commentRepository, IPhotoRepository photoRepository)
    {
        _commentRepository = commentRepository;
        _photoRepository = photoRepository;
    }

    public async Task<CommentDto> AddCommentAsync(AddCommentDto dto)
    {
        var photo = await _photoRepository.GetByIdAsync(dto.PhotoId);
        if( photo is null)
            throw new NotFoundException("Photo not found");
        
        var comment = await _commentRepository.AddAsync(dto);

        return comment;
    }

    public async Task<List<CommentDto>> GetCommentByPhotoIdAsync(Guid photoId)
    {
        return await _commentRepository.GetByPhotoIdAsync(photoId);
    }

    public async Task DeleteCommentAsync(Guid commentId, Guid requestingUserId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        if (comment is null)
            throw new NotFoundException("Comment not found");
        
        if (comment.UserId != requestingUserId)
            throw new ForbiddenAccessException("You are not authorized to delete this comment");
        
        await _commentRepository.DeleteAsync(commentId);
    }
    
}