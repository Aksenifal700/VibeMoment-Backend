using System.Security.Claims;
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
    public async Task<ActionResult<PhotoResponse>> GetPhoto([FromRoute] int id)
    {
        var photo = await _photoService.GetPhotoAsync(id);

        var response = _mapper.Map<PhotoResponse>(photo);
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<PhotoResponse>> UploadPhoto([FromForm] UploadPhotoRequest request)
    {
        var uploadDto = await PrepareUploadDto(request);

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

    [HttpGet]
    public async Task<ActionResult<List<PhotoDto>>> GetPhotos([FromQuery] PhotosQuery query)
    {
        var photos = await _photoService.GetPhotosByUserIdAsync(query);
        return Ok(photos);
    }

    private async Task<UploadPhotoDto> PrepareUploadDto(UploadPhotoRequest request)
    { 
        using var stream = new MemoryStream();
        await request.Photo.CopyToAsync(stream);

        var uploadDto = _mapper.Map<UploadPhotoDto>(request);
        uploadDto.Data = stream.ToArray();
        uploadDto.FileName = request.Photo.FileName;
        uploadDto.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        return uploadDto;
    }
}