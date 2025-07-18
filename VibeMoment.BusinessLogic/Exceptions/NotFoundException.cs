using System.Net;
namespace VibeMoment.BusinessLogic.Exceptions;

public class NotFoundException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    
    public NotFoundException(string message) : base(message) { }
}

public class UserNotFoundException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    
    public UserNotFoundException(string message) : base(message) { }
}

