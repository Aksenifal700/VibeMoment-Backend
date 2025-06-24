using System.Net;

namespace VibeMoment.BusinessLogic. Exceptions;
public class BusinessLogicException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    
    public BusinessLogicException(string message) : base(message) { }
}