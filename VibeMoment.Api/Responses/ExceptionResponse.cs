using System.Net;

namespace VibeMoment.Api.Responses;

public record ExceptionResponse(HttpStatusCode StatusCode, string Message);
