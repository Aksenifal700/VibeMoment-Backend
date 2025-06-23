using System.Net;

namespace VibeMoment.Api.Models.Responses;

public record ExceptionResponse(HttpStatusCode StatusCode, string Message);
