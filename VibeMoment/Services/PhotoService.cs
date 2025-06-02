using AutoMapper;
using VibeMoment.Database;
using VibeMoment.Database.Entities;
using VibeMoment.Requests;
using VibeMoment.Services.Interfaces;


namespace VibeMoment.Services;

public class PhotoService : IPhotoService
{
   private readonly AppDbContext _dbContext;
   private readonly IMapper _mapper;

   public PhotoService(AppDbContext dbContext, IMapper mapper)
   {
      _dbContext = dbContext;
      _mapper = mapper;
   }

   public async Task<Photo?> GetPhotoAsync(int id)
   {
      return await _dbContext.Photos.FindAsync(id);
   }

   public async Task<Photo> SavePhotoAsync(SavePhotoRequest request)
   {
      var photo = _mapper.Map<Photo>(request);
      await _dbContext.Photos.AddAsync(photo);
      await _dbContext.SaveChangesAsync();
      return photo;
   }

   public async Task<Photo> UploadPhotoAsync(UploadPhotoDto dto)
   {
      using var ms = new MemoryStream();
      await dto.File.CopyToAsync(ms);
      var bytes = ms.ToArray();

      var photo = new Photo
      {
         Name = dto.Name,
         Data = bytes
      };

      _dbContext.Photos.Add(photo);
      await _dbContext.SaveChangesAsync();

      return photo;
   }

   public async Task<Photo?> UpdatePhotoAsync(int id, UpdatePhotoRequest update)
   {
      var photo = await _dbContext.Photos.FindAsync(id);
      if (photo == null) return null;

      photo.Name = update.Title;
      await _dbContext.SaveChangesAsync();

      return photo;
   }

   public async Task<bool> DeletePhotoAsync(int id)
   {
      var photo = await _dbContext.Photos.FindAsync(id);
      if (photo == null) return false;

      _dbContext.Photos.Remove(photo);
      await _dbContext.SaveChangesAsync();

      return true;
   }
}
