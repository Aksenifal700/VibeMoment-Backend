namespace VibeMoment.BusinessLogic.DTOs.Photo;

public class AddCommentDto
{
    public string Content { get; set; }
    
    public Guid UserId { get; set; }
    public Guid PhotoId { get; set; }
}