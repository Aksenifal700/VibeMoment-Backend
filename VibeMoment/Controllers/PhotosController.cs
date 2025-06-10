using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VibeMoment.Requests;
using VibeMoment.Services.Interfaces;
using VibeMoment.ExceptionsHandlers;

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
        try
        {
            var photo = await _photoService.GetPhotoAsync(id);
            return File(photo.Data, "image/jpeg");
        }
        catch (NotFoundException)
        {
            return NotFound($"Photo with id {id} not found");
        }
    }

    [HttpPost("upload")]
    [Authorize]
    public async Task<ActionResult> UploadPhotoRequest([FromForm] UploadPhotoRequest request)
    {
        if (request.Photo.Length is 0)
            return BadRequest("Photo required");

        var photo = await _photoService.UploadPhotoAsync(request);

        return CreatedAtAction(nameof(GetPhoto), new { id = photo.Id }, photo);
        
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<ActionResult> UpdatePhoto([FromRoute] int id, [FromBody] UpdatePhotoRequest request)
    {
        var isUpdated = await _photoService.UpdatePhotoAsync(id, request);
    
        return isUpdated 
            ? Ok("Photo updated") 
            : BadRequest("Photo not found or edit time expired");
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<ActionResult> DeletePhoto([FromRoute] int id)
    {
        var deleted = await _photoService.DeletePhotoAsync(id);
        return deleted? 
            NoContent(): 
            NotFound();
    }
}
