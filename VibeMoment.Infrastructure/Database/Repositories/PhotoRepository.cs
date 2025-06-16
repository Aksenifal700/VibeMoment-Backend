using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VibeMoment.BusinessLogic.DTOs;
using VibeMoment.BusinessLogic.Services.Interfaces;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.Database.Repositories;

public class PhotoRepository : IPhotoRepository
{
    
    private const int EDIT_LIMIT_HOURS = 1;
    
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PhotoRepository> _logger;
    
    public PhotoRepository(AppDbContext context, IMapper mapper, ILogger<PhotoRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PhotoDto?> GetByIdAsync(int id) {
        var photo = await _context.Photos.FindAsync(id);
    
        if (photo is null) {
            return null;
        }

        return _mapper.Map<PhotoDto>(photo);  
    }

    public async Task<PhotoDto> SavePhotoAsync(UploadPhotoDto request) {
        var photo = new Photo {
            Title = request.Title,
            Data = request.PhotoData,
            AddedAt = DateTime.UtcNow
        };

        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();
    
        return _mapper.Map<PhotoDto>(photo);
    }
    public async Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto request)
    {
        var updatedCount = await _context.Photos
            .Where(p => p.Id == request.Id && p.AddedAt >= DateTime.UtcNow.AddHours(-EDIT_LIMIT_HOURS))  // ← request.Id
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.Title, request.Title)
                .SetProperty(p => p.UpdatedAt, DateTime.UtcNow));

        if (updatedCount > 0)
        {
            _logger.LogInformation("Photo {Id} updated", request.Id);  
            var photo = await _context.Photos.FindAsync(request.Id);  
            return _mapper.Map<PhotoDto>(photo);
        }

        return null;
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