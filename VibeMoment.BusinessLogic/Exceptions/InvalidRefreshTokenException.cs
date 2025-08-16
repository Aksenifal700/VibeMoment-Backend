using System.Net;

namespace VibeMoment.BusinessLogic.Exceptions;

public class InvalidRefreshTokenException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
    private InvalidRefreshTokenException(string message) : base(message) { }
    
    public static InvalidRefreshTokenException Invalid() =>
        new InvalidRefreshTokenException("Invalid refresh token.");

    public static InvalidRefreshTokenException RevokedOrMissing() =>
        new InvalidRefreshTokenException("Token not found or already revoked.");
}