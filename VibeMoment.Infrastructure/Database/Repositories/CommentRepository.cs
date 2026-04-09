using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.Database.Repositories;

public class CommentRepository : ICommentRepository 
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CommentRepository> _logger;
    
    public CommentRepository(AppDbContext context, IMapper mapper, ILogger<CommentRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CommentDto> AddAsync(AddCommentDto dto)
    {
        var comment = _mapper.Map<Comment>(dto);
        comment.CreatedAt = DateTime.UtcNow;
        
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return _mapper.Map<CommentDto>(comment); 
    }

    public async Task<List<CommentDto>> GetByPhotoIdAsync(Guid photoId)
    {
        var comment = _context.Comments
            .Where(c => c.PhotoId == photoId);

        var result = await comment.ToListAsync();
        
        return _mapper.Map<List<CommentDto>>(result);
    }
    
    public async  Task<CommentDto?> GetByIdAsync(Guid commentId)
    {
        var comment = await _context.Comments.FindAsync(commentId);
        return comment is null
            ? null
            : _mapper.Map<CommentDto>(comment);
    }

    public async Task<bool> DeleteAsync(Guid commentId)
    {
        var deletedCount = await _context.Comments
            .Where(c => c.Id == commentId)
            .ExecuteDeleteAsync();
        
        return deletedCount > 0;
    }
}