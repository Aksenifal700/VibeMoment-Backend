using VibeMoment.Api.Models.Responses;
using VibeMoment.BusinessLogic.Exceptions;

namespace VibeMoment.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unexpected error occurred.");
        
        var response = exception switch
        {
            NotFoundException notFoundEx => new ExceptionResponse(notFoundEx.StatusCode, notFoundEx.Message),
            BusinessLogicException businessEx => new ExceptionResponse(businessEx.StatusCode, businessEx.Message),
            UserNotFoundException notFoundEx => new ExceptionResponse(notFoundEx.StatusCode, notFoundEx.Message),
            InvalidRefreshTokenException notFoundEx => new  ExceptionResponse(notFoundEx.StatusCode, notFoundEx.Message)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }

}