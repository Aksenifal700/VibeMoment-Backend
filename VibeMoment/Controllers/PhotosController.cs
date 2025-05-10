using Microsoft.AspNetCore.Mvc;

namespace VibeMoment.Controllers;

[ApiController]
[Route("[controller]")]
public class PhotosController
{
    private static readonly List<Photo> _photos = new();

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
    public ActionResult<bool> SavePhoto([FromBody] Photo photoRequest)
    {
        if (photoRequest.Id != 0 &&
            !_photos.Exists(photo => photo.Id == photoRequest.Id))
        {
            _photos.Add(photoRequest);
            return true;
        }

        return false;
    }
}