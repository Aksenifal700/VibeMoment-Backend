using VibeMoment.ExceptionsHandlers;
using Microsoft.EntityFrameworkCore;
using VibeMoment.Database;
using VibeMoment.Database.Entities;
using VibeMoment.Extensions;
using VibeMoment.Requests;
using VibeMoment.Services.Interfaces;


namespace VibeMoment.Services;

public class PhotoService : IPhotoService
{
    private readonly AppDbContext _context;
    private readonly ILogger<PhotoService> _logger;
    
    private const int EDIT_LIMIT_HOURS = 1;

    public PhotoService(AppDbContext context, ILogger<PhotoService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Photo?> GetPhotoAsync(int id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
        
        if (photo is null)
            throw new NotFoundException($"Photo with id {id} was not found");
        return photo;
    }

    public async Task<Photo> UploadPhotoAsync(UploadPhotoRequest request)
    {
        using var stream = new MemoryStream();
        await request.Photo.CopyToAsync(stream);
        
        var photo = new Photo
        {
            Title = request.Title,
            Data = stream.ToArray()
        };

        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Photo {Id} uploaded: {FileName}", photo.Id, request.Photo.FileName);
        return photo;
    }

    public async Task<bool> UpdatePhotoAsync(int id, UpdatePhotoRequest request)
    {
        var updatedCount = await _context.Photos
            .Where(p => p.Id == id && p.AddedAt >= DateTime.UtcNow.AddHours(-EDIT_LIMIT_HOURS))
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.Title, request.Title)
                .SetProperty(p => p.UpdatedAt, DateTime.UtcNow));

        if (updatedCount > 0)
        {
            _logger.LogInformation("Photo {Id} updated", id);
            return true;
        }
        _logger.LogInformation("Photo {Id} - cannot edit, because of time limit", id);
        return false;
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
        _logger.LogInformation("Photo not found id:{Id} not deleted", id);
        return false;
    }
}