using Microsoft.AspNetCore.Mvc;
using VibeMoment.Requests;
using VibeMoment.Services.Interfaces;

namespace VibeMoment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoService _photoService;

    public PhotosController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetPhoto([FromRoute] int id, [FromQuery] bool image = false)
    {
        var photo = await _photoService.GetPhotoAsync(id);
        
        if (photo is null) 
            return NotFound();
        
        if (image)
            return File(photo.Data, "image/jpeg");
        
        return Ok(new 
        { 
            photo.Id,
            photo.Title,
            photo.AddedAt,
            photo.TimeAgo,
            photo.CanEdit
        });
    }

    [HttpPost("upload")]
    public async Task<ActionResult> UploadPhoto([FromForm] UploadPhotoDto dto)
    {
        if (dto.Photo?.Length == 0)
            return BadRequest("Photo required");

        var photo = await _photoService.UploadPhotoAsync(dto);
        
        return CreatedAtAction(nameof(GetPhoto), new { id = photo.Id }, new 
        { 
            photo.Id, 
            photo.Title,
            photo.AddedAt,
            photo.CanEdit
        });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdatePhoto([FromRoute] int id, [FromBody] UpdatePhotoRequest request)
    {
        var photo = await _photoService.UpdatePhotoAsync(id, request);
        
        return photo is null 
            ? BadRequest("Photo not found or edit time expired") 
            : Ok(new { photo.Id, photo.Title, photo.CanEdit });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePhoto([FromRoute] int id)
    {
        var deleted = await _photoService.DeletePhotoAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}