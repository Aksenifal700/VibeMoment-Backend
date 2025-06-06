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
        return photo;
    }

    public async Task<Photo> UploadPhotoAsync(UploadPhotoRequest dto)
    {
        using var stream = new MemoryStream();
        await dto.Photo.CopyToAsync(stream);
        
        var photo = new Photo
        {
            Title = dto.Title,
            Data = stream.ToArray()
        };

        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Photo {Id} uploaded: {FileName}", photo.Id, dto.Photo.FileName);
        return photo;
    }

    public async Task<Photo?> UpdatePhotoAsync(int id, UpdatePhotoRequest request)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
        
        if (photo is null || !photo.AddedAt.CanEdit(EDIT_LIMIT_HOURS))
        {
            _logger.LogWarning("Photo {Id} - cannot edit, because time limit restriction", photo.Id);
            return null;
        }

        photo.Title = request.Title;
        photo.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Photo {Id} updated", photo.Id);
        return photo;
    }

    public async Task<bool> DeletePhotoAsync(int id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
        if (photo is null)
            return false;

        _context.Photos.Remove(photo);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Photo with id:{Id} deleted", photo.Id);
        return true;
    }
}