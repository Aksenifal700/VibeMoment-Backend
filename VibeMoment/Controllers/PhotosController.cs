using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VibeMoment.Database.Entities;
using VibeMoment.Requests;
using VibeMoment.Services.Interfaces;

namespace VibeMoment.Controllers;

[ApiController]
[Route("photos")]
public class PhotosController
{
    private static readonly List<Photo> _photos = new();
    
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public PhotosController( //Dependency injection - розібратись що це таке 
        IMapper mapper, 
        IPhotoService photoService)
    {
        _mapper = mapper;
        _photoService = photoService;
    }


    [HttpGet("{id:int}")]
    public ActionResult<Photo> GetPhoto([FromRoute] int id)
    {
        var photo = _photos.FirstOrDefault(photo => photo.Id == id);
        return photo;

        /*for (int i = 0; i < _photos.Count; i++)
        {
            if (_photos[i].Id == id)
            {
                return _photos[i];
            }
        }

        return null;*/
    }

    [HttpPost]
    public ActionResult<bool> SavePhoto([FromBody] SavePhotoRequest request)
    {
        // Симуляція створення рандомного id як це буде робити база данних
        var id = Random.Shared.Next(1, 10000000);

        //використання автомаппера
        var photo = _mapper.Map<Photo>(request);
        
        // Ручний мапінг
        /*var photo = new Photo
        {
            Id = id, 
            Name = request.Name
        };*/
        
        
        _photos.Add(photo);
        
        //виклик методу з сервісу в який треба винести логіку щоб вона не була в контроллері 
        _photoService.SavePhoto(request);
        
        return true;
    }
}