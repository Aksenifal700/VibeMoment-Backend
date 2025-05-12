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
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public PhotosController(AppDbContext dbContext, IMapper mapper, IPhotoService photoService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _photoService = photoService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Photo>> GetPhoto([FromRoute] int id)
    {
        var photo = await _dbContext.Photos.FindAsync(id);
        if (photo == null) 
            return NotFound();
        return Ok(photo);
    }

    [HttpPost]
    public async Task<ActionResult<Photo>> SavePhoto([FromBody] SavePhotoRequest request)
    {
        var photo = _mapper.Map<Photo>(request);
        await _dbContext.Photos.AddAsync(photo);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPhoto), new { id = photo.Id }, photo);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Photo>> UpdatePhoto([FromRoute] int id, [FromBody] UpdatePhotoRequest updatePhoto)
    {
        var photo = await _dbContext.Photos.FindAsync(id);
        if (photo == null) 
            return NotFound();

        photo.Name = updatePhoto.Name;
        await _dbContext.SaveChangesAsync();
        return Ok(photo);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePhoto([FromRoute]int id)
    {
        var photo = await _dbContext.Photos.FindAsync(id);
        if (photo == null) 
            return NotFound();

        _dbContext.Photos.Remove(photo);
        return NoContent();
    }
}

