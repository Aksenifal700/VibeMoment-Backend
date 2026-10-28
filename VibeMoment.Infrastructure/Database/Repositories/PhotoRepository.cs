using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VibeMoment.BusinessLogic.DTOs.Common;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.BusinessLogic.Enums;
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

    public async Task<PhotoDto?> GetByIdAsync(Guid id)
    {
        var photo = await _context.Photos.FindAsync(id);
        return photo is null
            ? null
            : _mapper.Map<PhotoDto>(photo);
    }

    public async Task<PhotoDto> SaveAsync(UploadPhotoDto dto)
    {
        var photo = _mapper.Map<Photo>(dto);
        photo.AddedAt = DateTime.UtcNow;

        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();

        return _mapper.Map<PhotoDto>(photo);
    }

    public async Task<PhotoDto> UpdateAsync(UpdatePhotoDto dto)
    {
       var existingPhoto = await _context.Photos.FindAsync(dto.Id);
        if (existingPhoto is null) 
            throw new NotFoundException("Photo not found");

        _mapper.Map(dto, existingPhoto); 

        await _context.SaveChangesAsync();

        _logger.LogInformation("Photo {Id} updated", dto.Id);

        return _mapper.Map<PhotoDto>(existingPhoto);
    }

    public async Task<bool> DeleteAsync(Guid id)
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

    public async Task<PageResult<PhotoDto>> GetByUserIdAsync(PhotosQueryDto queryDto)
    {
        var photoQuery = _context.Photos
            .Where(p => p.UserId == queryDto.UserId);
        
        if (!string.IsNullOrEmpty(queryDto.SearchTerm))
        photoQuery = photoQuery.Where(p =>
            EF.Functions.ILike(p.Title, $"%{queryDto.SearchTerm}%") ||
            EF.Functions.ILike(p.Description, $"%{queryDto.SearchTerm}%"));         
        
        var totalCount = await photoQuery.CountAsync();

        photoQuery = queryDto.SortBy switch
        {
            PhotoSortBy.Title => queryDto.OrderBy == OrderDirection.Asc
                ? photoQuery.OrderBy(p => p.Title)
                : photoQuery.OrderByDescending(p => p.Title),

            PhotoSortBy.AddedAt => queryDto.OrderBy == OrderDirection.Asc
                ? photoQuery.OrderBy(p => p.AddedAt)
                : photoQuery.OrderByDescending(p => p.AddedAt),

            PhotoSortBy.UpdatedAt => queryDto.OrderBy == OrderDirection.Asc
                ? photoQuery.OrderBy(p => p.UpdatedAt)
                : photoQuery.OrderByDescending(p => p.UpdatedAt)
        };
        
       photoQuery = photoQuery
            .Skip((queryDto.PageNumber - 1) * queryDto.PageSize)
            .Take(queryDto.PageSize);

        var result = await photoQuery.ToListAsync();
        
        var mappedPhotos = _mapper.Map<List<PhotoDto>>(result);
        return new PageResult<PhotoDto>(mappedPhotos, totalCount, queryDto.PageNumber, queryDto.PageSize);
    }
}