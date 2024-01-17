using Shared;
using System.Net;

namespace BackgroundService.Middleware;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ArgumentNullException ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (ArgumentException ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, string message, HttpStatusCode statusCode)
    {
        _logger.LogError($"Error occured - {message}");

        var response = httpContext.Response;

        response.ContentType = "application/json";
        response.StatusCode = (int)statusCode;

        await response.WriteAsync(new BaseResponseModel
        {
            Message = message,
            StatusCode = statusCode
        }.ToString()!);
    }
}
