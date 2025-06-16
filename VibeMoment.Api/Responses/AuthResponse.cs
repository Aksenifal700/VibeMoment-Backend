namespace VibeMoment.Api.Responses;

public class AuthResponse
{
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? UserId { get; set; }
        public List<string> Errors { get; set; } = new();
    
}