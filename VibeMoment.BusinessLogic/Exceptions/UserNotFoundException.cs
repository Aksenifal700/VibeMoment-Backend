using System.Net;

namespace VibeMoment.BusinessLogic.Exceptions;

public class UserNotFoundException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public UserNotFoundException()
        : base("Invalid username or password.")
    {
    }
}