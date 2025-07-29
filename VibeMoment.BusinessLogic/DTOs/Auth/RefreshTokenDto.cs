namespace VibeMoment.BusinessLogic.DTOs.Auth;

public class RefreshTokenDto
{
    public string Email  { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresOnUtc { get; set; }
    public Guid UserId { get; set; }
    public bool IsRevoked { get; set; }
}