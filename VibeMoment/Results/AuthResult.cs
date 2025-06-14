using Microsoft.AspNetCore.Identity;

namespace VibeMoment.Results;

public class AuthResult
{
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? UserId { get; set; }
        public IEnumerable<IdentityError>? Errors { get; set; }
    
}