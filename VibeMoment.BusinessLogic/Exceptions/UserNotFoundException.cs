using System.Net;

namespace VibeMoment.BusinessLogic.Exceptions;

public class UserNotFoundException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public UserNotFoundException() : base("User not found") {}
}