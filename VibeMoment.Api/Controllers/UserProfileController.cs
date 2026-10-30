
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VibeMoment.BusinessLogic.DTOs.Auth;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;

    public UserProfileController(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    [AllowAnonymous]
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserProfileDto?>> GetProfile([FromRoute] Guid userId)
    {
        var profile = await _userProfileService.GetProfileAsync(userId);

        return Ok(profile);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<UserProfileDto?>> UpdateProfile([FromBody] UpdateUserProfileDto dto)
    {
        var currentUserId = Guid.Parse(User.FindFirst("userid")!.Value!);
        
        var result = await _userProfileService.UpdateProfileAsync(dto, currentUserId);
        return Ok(result);
    }

    [Authorize]
    [HttpPatch("avatar")]
    public async Task<ActionResult<UserProfileDto?>> UpdateAvatar([FromForm] IFormFile avatar)
    {
        var currentuserId = Guid.Parse(User.FindFirst("userid")!.Value!);
        
        using var stream = new MemoryStream();
        await avatar.CopyToAsync(stream);
        
        var result = await _userProfileService.UploadAvatarAsync(currentuserId, stream.ToArray());
        return Ok(result);
    }
}