using System.Net;
namespace VibeMoment.BusinessLogic.Exceptions;

public class NotFoundException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    
    public NotFoundException(string message) : base(message) { }
}
public class BusinessLogicException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    
    public BusinessLogicException(string message) : base(message) { }
}