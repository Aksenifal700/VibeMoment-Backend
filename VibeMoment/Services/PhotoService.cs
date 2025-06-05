using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VibeMoment.Database;
using VibeMoment.Database.Entities;
using VibeMoment.Requests;
using VibeMoment.Services.Interfaces;

namespace VibeMoment.Services;

public class PhotoService : IPhotoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PhotoService> _logger;

    public PhotoService(AppDbContext context, IMapper mapper, ILogger<PhotoService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Photo?> GetPhotoAsync(int id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

        if (photo is not null)
        {
            _logger.LogInformation("Photo {PhotoId} retrieved. Added: {AddedAt}, Time since added: {TimeSinceAdded}",
                photo.Id, photo.AddedAt, photo.TimeAgo);
        }

        return photo;
    }

    public async Task<Photo> UploadPhotoAsync(UploadPhotoDto dto)
    {
        using var memoryStream = new MemoryStream();
        await dto.Photo.CopyToAsync(memoryStream);

        var currentTime = DateTime.UtcNow;
        var photo = new Photo
        {
            Title = dto.Title ?? Path.GetFileNameWithoutExtension(dto.Photo.FileName),
            Data = memoryStream.ToArray(),
            AddedAt = currentTime 
        };

        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Photo {PhotoId} uploaded at {AddedAt}. File: {FileName}, Size: {FileSize} bytes",
            photo.Id, photo.AddedAt, dto.Photo.FileName, dto.Photo.Length);

        return photo;
    }

    public async Task<Photo?> UpdatePhotoAsync(int id, UpdatePhotoRequest request)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
        if (photo is null) return null;

        photo.Title = request.Title;
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Photo {PhotoId} updated. Originally added {TimeAgo} ({AddedAt})",
            photo.Id, photo.TimeAgo, photo.AddedAt);

        return photo;
    }

    public async Task<bool> DeletePhotoAsync(int id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
        if (photo is null) return false;
        
        _logger.LogInformation("Deleting photo {PhotoId}. Photo existed for {TimeSinceAdded} (added at {AddedAt})",
            photo.Id, photo.TimeAgo, photo.AddedAt);

        _context.Photos.Remove(photo);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<IEnumerable<Photo>> GetPhotosByAgeAsync(int olderThanDays = 0)
    {
        var photos = await _context.Photos.ToListAsync();
        var filteredPhotos = photos.Where(p => p.DaysOld >= olderThanDays).ToList();

        _logger.LogInformation("Found {Count} photos older than {Days} days",
            filteredPhotos.Count, olderThanDays);

        return filteredPhotos;
    }
}