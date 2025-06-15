using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VibeMoment.BusinessLogic.Requests;
using VibeMoment.BusinessLogic.Services.Interfaces;

namespace VibeMoment.Api.Controllers;

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
        
        if (photo is null)
        {
            return
                NotFound($"Photo with id {id} not found");
        }

        return Ok(photo);
    }

    [HttpPost("upload")]
    [Authorize]
    public async Task<ActionResult> UploadPhotoRequest(IFormFile photo, string title)
    {
        if (photo?.Length is 0 or null)
            return BadRequest("Photo required");
    
        using var stream = new MemoryStream();
        await photo.CopyToAsync(stream);

        var request = new UploadPhotoRequest
        {
            Title = title,
            PhotoData = stream.ToArray(),
            FileName = photo.FileName
        };

        var result = await _photoService.UploadPhotoAsync(request);

        return CreatedAtAction(nameof(GetPhoto), new { id = result.Id }, result);
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
        return deleted ? NoContent() : NotFound();
    }
}