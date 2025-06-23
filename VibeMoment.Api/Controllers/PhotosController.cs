using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using VibeMoment.Api.Models.Requests.Photo;
using VibeMoment.Api.Models.Responses;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.BusinessLogic.Interfaces.Services;

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
        
        var response = _mapper.Map<PhotoResponse>(photo);
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<PhotoResponse>> UploadPhoto([FromForm] UploadPhotoRequest request)
    {
        if (request.Photo.Length is 0)
        {
            return BadRequest();
        }

        using var stream = new MemoryStream();
        await request.Photo.CopyToAsync(stream);

        var uploadDto = _mapper.Map<UploadPhotoDto>(request);
        uploadDto.Data = stream.ToArray();
        uploadDto.FileName = request.Photo.FileName;
        
        var result = await _photoService.UploadPhotoAsync(uploadDto);
        
        var response = _mapper.Map<PhotoResponse>(result);
        
        return CreatedAtAction(
            nameof(GetPhoto), 
            new { id = response.Id }, 
            response
        );
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<ActionResult<PhotoResponse>> UpdatePhoto([FromRoute] int id,
        [FromBody] UpdatePhotoRequest request)
    {
        var updateDto = _mapper.Map<UpdatePhotoDto>(request);
        updateDto.Id = id;

        var updatedPhoto = await _photoService.UpdatePhotoAsync(updateDto);
        
        var photoResponse = _mapper.Map<PhotoResponse>(updatedPhoto);
        return Ok(photoResponse);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<ActionResult> DeletePhoto([FromRoute] int id)
    {
        await _photoService.DeletePhotoAsync(id);
        return Ok();
        
    }
}