using VibeMoment.BusinessLogic.DTOs;
using VibeMoment.BusinessLogic.Services.Interfaces;

namespace VibeMoment.BusinessLogic.Services;

    public class PhotoService : IPhotoService
    {
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
            return await _photoRepository.UpdatePhotoAsync(updateDto);
        }

        public async Task<bool> DeletePhotoAsync(int id)
        {
            return await _photoRepository.DeletePhotoAsync(id);
        }
        
    }
