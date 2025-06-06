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
        
        if (photo != null)
            _logger.LogInformation("Photo {Id} - {TimeAgo}, CanEdit: {CanEdit}", 
                photo.Id, photo.TimeAgo, photo.CanEdit);
        
        return photo;
    }

    public async Task<Photo> UploadPhotoAsync(UploadPhotoDto dto)
    {
        using var stream = new MemoryStream();
        await dto.Photo.CopyToAsync(stream);
        
        var photo = new Photo
        {
            Title = dto.Title ?? Path.GetFileNameWithoutExtension(dto.Photo.FileName),
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
        
        if (photo == null || !photo.CanEdit)
        {
            _logger.LogWarning("Photo {Id} - cannot edit (added {TimeAgo})", id, photo?.TimeAgo);
            return null;
        }

        photo.Title = request.Title;
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Photo {Id} updated", photo.Id);
        return photo;
    }

    public async Task<bool> DeletePhotoAsync(int id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
        if (photo == null) return false;

        _context.Photos.Remove(photo);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Photo {Id} deleted (existed {TimeAgo})", photo.Id, photo.TimeAgo);
        return true;
    }
}