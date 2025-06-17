using VibeMoment.BusinessLogic.DTOs.Photodtos;
using VibeMoment.BusinessLogic.Interfaces.Repositories;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.BusinessLogic.Services;

    public class PhotoService : IPhotoService
    {
        private const int EDIT_LIMIT_HOURS = 1;
        
        private readonly IPhotoRepository _photoRepository;  
        
        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;  
        }

        public async Task<PhotoDto?> GetPhotoAsync(int id)  
        {
            return await _photoRepository.GetByIdAsync(id);
        }

        public async Task<PhotoDto> UploadPhotoAsync(UploadPhotoDto uploadDto) 
        {
            return await _photoRepository.SavePhotoAsync(uploadDto);
        }

        public async Task<PhotoDto> UpdatePhotoAsync(UpdatePhotoDto updateDto)  
        {
            var photo = await _photoRepository.GetByIdAsync(updateDto.Id);
            if (photo is null)
                  return null; 
        
            
           
            if ((DateTime.UtcNow - photo.AddedAt).TotalHours < EDIT_LIMIT_HOURS)
            {
                updateDto.UpdatedAt = DateTime.UtcNow;
            }
            return await _photoRepository.UpdatePhotoAsync(updateDto);
        }

        public async Task<bool> DeletePhotoAsync(int id)
        {
            return await _photoRepository.DeletePhotoAsync(id);
        }
        
    }
