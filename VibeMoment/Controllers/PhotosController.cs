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
    public async Task<ActionResult> GetPhoto([FromRoute] int id)
    {
        var photo = await _photoService.GetPhotoAsync(id);
        return photo is null ? NotFound() : File(photo.Data, "image/jpeg");
    }

    [HttpPost("upload")]
    public async Task<ActionResult> UploadPhoto([FromForm] UploadPhotoDto dto)
    {
        if (dto.Photo is null || dto.Photo.Length is 0)
            return BadRequest("Photo file is required");

        var photo = await _photoService.UploadPhotoAsync(dto);
        return CreatedAtAction(nameof(GetPhoto), new { id = photo.Id }, new { id = photo.Id, title = photo.Title });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdatePhoto([FromRoute] int id, [FromBody] UpdatePhotoRequest request)
    {
        var photo = await _photoService.UpdatePhotoAsync(id, request);
        return photo is null ? NotFound() : Ok(new { id = photo.Id, title = photo.Title });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePhoto([FromRoute] int id)
    {
        var deleted = await _photoService.DeletePhotoAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}