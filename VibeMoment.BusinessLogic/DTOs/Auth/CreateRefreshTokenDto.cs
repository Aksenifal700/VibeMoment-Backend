namespace VibeMoment.BusinessLogic.DTOs.Auth;

public class CreateRefreshTokenDto
{
    public string Token { get; set; }
    public DateTime ExpiresOnUtc { get; set; }
    public Guid UserId { get; set; }
}