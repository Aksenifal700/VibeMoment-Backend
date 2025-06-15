using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VibeMoment.BusinessLogic.Requests;
using VibeMoment.BusinessLogic.Results;
using VibeMoment.BusinessLogic.Services.Interfaces;
using VibeMoment.Infrastructure.Database;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.Repositories;

public class PhotoRepository : IPhotoRepository
{
    
    private const int EDIT_LIMIT_HOURS = 1;
    
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PhotoRepository> _logger;
    private IPhotoRepository _photoRepositoryImplementation;


    public PhotoRepository(AppDbContext context, IMapper mapper, ILogger<PhotoRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PhotoResult?> GetByIdAsync(int id)
    {
        var photo = await _context.Photos.FindAsync(id);
        if (photo is null)
        {
            return new PhotoResult { Success = false, ErrorMessage = "Photo not found" };
        }

        return _mapper.Map<PhotoResult>(photo);
    }

    public async Task<PhotoResult> SavePhotoAsync(UploadPhotoRequest request)
    {
        var photo = new Photo
        {
            Title = request.Title,
            Data = request.PhotoData,
            AddedAt = DateTime.UtcNow
        };

        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Photo {Id} uploaded: {Title}", photo.Id, request.Title);
        
        return _mapper.Map<PhotoResult>(photo);
    }

    public async Task<PhotoResult> UpdatePhotoAsync(int id, UpdatePhotoRequest request)
    {
        var updatedCount = await _context.Photos
            .Where(p => p.Id == id && p.AddedAt >= DateTime.UtcNow.AddHours(-EDIT_LIMIT_HOURS))
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.Title, request.Title)
                .SetProperty(p => p.UpdatedAt, DateTime.UtcNow));

        if (updatedCount > 0)
        {
            _logger.LogInformation("Photo {Id} updated", id);
            var photo = await _context.Photos.FindAsync(id);
            return _mapper.Map<PhotoResult>(photo);
        }

        return new PhotoResult { Success = false, ErrorMessage = "Cannot edit - time limit exceeded" };
    }

    public async Task<bool> DeletePhotoAsync(int id)
    {
        var deletedCount = await _context.Photos
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        if (deletedCount > 0)
        {
            _logger.LogInformation("Photo with id:{Id} deleted", id);
            return true;
        }

        return false;
    }
}