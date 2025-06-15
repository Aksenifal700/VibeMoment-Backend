namespace VibeMoment.BusinessLogic.Results;

public class AuthResult
{
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? UserId { get; set; }
        public List<string> Errors { get; set; } = new();
    
}