using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VibeMoment.Api.Models.Requests.Photo;
using VibeMoment.Api.Models.Responses;
using VibeMoment.BusinessLogic.DTOs.Photo;
using VibeMoment.BusinessLogic.Interfaces.Services;

namespace VibeMoment.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet("{photoId:guid}")]
    public async Task<ActionResult<List<CommentResponse>>> GetComment([FromRoute] Guid photoId)
    {
        var comment = await _commentService.GetCommentByPhotoIdAsync(photoId);

        var response = _mapper.Map<List<CommentResponse>>(comment);
        return Ok(response);
    }

    [Authorize]
    [HttpPost("{photoId:guid}")]
    public async Task<ActionResult<CommentResponse>> PostComment([FromRoute] Guid photoId,
        [FromBody] CommentRequest commentRequest)
    {
        var postDto = PrepareAddCommentDto(commentRequest, photoId);
        var result = await _commentService.AddCommentAsync(postDto);
        var response = _mapper.Map<CommentResponse>(result);

        return CreatedAtAction(
            nameof(GetComment),
            new { photoId = photoId },
            response
        );
    }

    [Authorize]
    [HttpDelete("{commentId:guid}")]
    public async Task<ActionResult> DeleteComment([FromRoute] Guid commentId)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue("userid"));
        await _commentService.DeleteCommentAsync(commentId, requestingUserId);
        return NoContent();
    }

    private AddCommentDto PrepareAddCommentDto(CommentRequest request, Guid photoId)
    {
        var postDto = new AddCommentDto();
        postDto.Content = request.Content;
        postDto.PhotoId = photoId;
        postDto.UserId = Guid.Parse(User.FindFirstValue("userid"));

        return postDto;
    }
}