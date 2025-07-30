using System.Net;

namespace VibeMoment.BusinessLogic.Exceptions;

public class ForbiddenAccessException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    
    public ForbiddenAccessException(string message) : base(message) { }
}