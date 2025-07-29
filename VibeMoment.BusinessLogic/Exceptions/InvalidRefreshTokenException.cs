using System.Net;

namespace VibeMoment.BusinessLogic.Exceptions;

public class InvalidRefreshTokenException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
    public InvalidRefreshTokenException(string message) : base(message) { }
}