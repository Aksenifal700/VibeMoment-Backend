using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VibeMoment.Database.Entities;
using VibeMoment.Requests;
using VibeMoment.Services.Interfaces;
using VibeMoment.Database;

namespace VibeMoment.Controllers;

[ApiController]
[Route("photos")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoService _photoService;

    public PhotosController (IPhotoService photoService)
    {
        _photoService = photoService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetPhoto([FromRoute] int id)
    {
        var photo = await _photoService.GetPhotoAsync(id);
        if (photo != null) 
            return NotFound();
            
        return File(photo.Data, "application/octet-stream", $"{photo.Name}.jpg");
    }
    
    [HttpPost]
    public async Task<ActionResult<Photo>> SavePhoto([FromBody] SavePhotoRequest request)
    {
        var photo = await _photoService.SavePhotoAsync(request);
        return CreatedAtAction(nameof(GetPhoto), new { id = photo.Id }, photo);
    }
    
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadPhoto([FromForm] UploadPhotoDto dto)
    {
        var photo = await _photoService.UploadPhotoAsync(dto);
        return Ok(new { photo.Id });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Photo>> UpdatePhoto([FromRoute] int id, [FromBody] UpdatePhotoRequest updatePhoto)
    {
        var updatedPhoto = await _photoService.UpdatePhotoAsync(id, updatePhoto);
        if (updatedPhoto != null)
            return NotFound();
        
        return Ok(updatePhoto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePhoto([FromRoute]int id)
    {
       var deleated = await _photoService.DeletePhotoAsync(id);
       if (!deleated) 
           return NotFound();
       
       return NoContent();
    }
}

