using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VibeMoment.Api.Requests;
using VibeMoment.Api.Responses;
using VibeMoment.BusinessLogic.Services.Interfaces;
using VibeMoment.BusinessLogic.DTOs;
using AutoMapper;
using System.Security.Claims;

namespace VibeMoment.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoService _photoService;
    private readonly IMapper _mapper;

    public PhotosController(IPhotoService photoService, IMapper mapper)
    {
        _photoService = photoService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetPhoto([FromRoute] int id)
    {
        var photo = await _photoService.GetPhotoAsync(id);  
    
        if (photo is null)
        {
            return NotFound(new { Success = false, Message = $"Photo with id {id} not found" });
        }

        return File(photo.Data, "image/jpeg");
    }

    [HttpPost("upload")]
    [Authorize]
    public async Task<ActionResult<UploadPhotoResponse>> UploadPhoto([FromForm] UploadPhotoRequest request)
    {
        if (request.Photo?.Length is 0 or null)
        {
            return BadRequest(new UploadPhotoResponse 
            { 
                Success = false, 
                Message = "",
                ErrorMessage = "Photo file is required" 
            });
        }

        using var stream = new MemoryStream();
        await request.Photo.CopyToAsync(stream);
        
        var uploadDto = _mapper.Map<UploadPhotoDto>(request);
        uploadDto.PhotoData = stream.ToArray();
        uploadDto.FileName = request.Photo.FileName ?? "";
       

        var result = await _photoService.UploadPhotoAsync(uploadDto);

        if (result is null)
        {
            return BadRequest(new UploadPhotoResponse 
            { 
                Success = false, 
                Message = "",
                ErrorMessage = "Failed to upload photo" 
            });
        }

        var photoResponse = _mapper.Map<PhotoResponse>(result);
        var response = new UploadPhotoResponse
        {
            Success = true,
            Message = "Photo uploaded successfully",
            ErrorMessage = "",
            Photo = photoResponse
        };

        return CreatedAtAction(nameof(GetPhoto), new { id = result.Id }, response);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<ActionResult<UpdatePhotoResponse>> UpdatePhoto([FromRoute] int id, [FromBody] UpdatePhotoRequest request)
    {
        var updateDto = _mapper.Map<UpdatePhotoDto>(request);
        updateDto.Id = id;

        var updatedPhoto = await _photoService.UpdatePhotoAsync(updateDto);  

        if (updatedPhoto is null)
        {
            return BadRequest(new UpdatePhotoResponse 
            { 
                Success = false, 
                Message = "",
                ErrorMessage = "Photo not found or edit time expired" 
            });
        }

        var photoResponse = _mapper.Map<PhotoResponse>(updatedPhoto);
        var response = new UpdatePhotoResponse
        {
            Success = true,
            Message = "Photo updated successfully",
            ErrorMessage = "",
            Photo = photoResponse
        };

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<ActionResult<DeletePhotoResponse>> DeletePhoto([FromRoute] int id)
    {
        var deleted = await _photoService.DeletePhotoAsync(id);
        
        if (!deleted)
        {
            return NotFound(new DeletePhotoResponse 
            { 
                Success = false, 
                Message = "",
                ErrorMessage = "Photo not found" 
            });
        }

        return Ok(new DeletePhotoResponse 
        { 
            Success = true, 
            Message = "Photo deleted successfully",
            ErrorMessage = ""
        });
    }
    
}