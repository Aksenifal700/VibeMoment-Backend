using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.BusinessLogic.Exceptions;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.Infrastructure.Database.Entities;

namespace VibeMoment.Infrastructure.Database.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PhotoRepository> _logger;

    public PhotoRepository(AppDbContext context, IMapper mapper, ILogger<PhotoRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PhotoDto?> GetByIdAsync(int id)
    {
        var photo = await _context.Photos.FindAsync(id);
        return photo is null
            ? null
            : _mapper.Map<PhotoDto>(photo);
    }

    public async Task<PhotoDto> SavePhotoAsync(UploadPhotoDto dto)
    {
        var photo = _mapper.Map<Photo>(dto);
        photo.AddedAt = DateTime.UtcNow;

        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();

        return _mapper.Map<PhotoDto>(photo);
    }

    public async Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto dto)
    {
       var existingPhoto = await _context.Photos.FindAsync(dto.Id);
        if (existingPhoto is null) 
            throw new NotFoundException("Photo not found");

        _mapper.Map(dto, existingPhoto); 

        await _context.SaveChangesAsync();

        _logger.LogInformation("Photo {Id} updated", dto.Id);

        return _mapper.Map<PhotoDto>(existingPhoto);
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