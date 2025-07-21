using System.Net;

namespace VibeMoment.BusinessLogic.Exceptions;

public class UserNotFoundException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    
    public UserNotFoundException(string message) : base(message) { }
}