using System.Text.Json;
using WebApi.Utilities;
using Microsoft.AspNetCore.Mvc.Controllers;
using Core.Beans;
using Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Middlewares;

/// <summary>
/// Middleware for handling exceptions globally in the application.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware to handle exceptions.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
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

    /// <summary>
    /// Retrieves the name of the controller handling the current request.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>The name of the controller, or "Unknown" if not available.</returns>
    private static string GetControllerName(HttpContext context)
    {
        Endpoint? endpoint = context.GetEndpoint();
        return endpoint?.Metadata?.GetMetadata<ControllerActionDescriptor>()?.ControllerName ?? Constants.UNKNOWN_CONTROLLER_NAME;
    }

    /// <summary>
    /// Retrieves the name of the action handling the current request.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>The name of the action, or "Unknown" if not available.</returns>
    private static string GetActionName(HttpContext context)
    {
        Endpoint? endpoint = context.GetEndpoint();
        return endpoint?.Metadata?.GetMetadata<ControllerActionDescriptor>()?.ActionName ?? Constants.UNKNOWN_ACTION_NAME;
    }

    /// <summary>
    /// Handles exceptions and prepares a response for the client.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <param name="exception">The exception to handle.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        IErrorLogService errorLogService = context.RequestServices.GetRequiredService<IErrorLogService>();
        (string ShortErrorMessage, int StatusCode) = ExceptionHelper.GetErrorShortModel(exception);
        
        ErrorLogDTO errorDetails = new()
        {
            ErrorMessage = exception.Message,
            StackTrace = exception.StackTrace,
            ExceptionType = exception.GetType().Name,
            ControllerName = GetControllerName(context),
            ActionName = GetActionName(context),
            StatusCode = StatusCode.ToString()
        };

        // Save error details to the error log database 
        await errorLogService.SaveErrorLogAsync(errorDetails);

        // Prepare the response
        ErrorResponse response = new()
        {
            Error = exception.Message,
            Message = ShortErrorMessage,
            Status = StatusCode
        };

        // Set response headers and content
        context.Response.ContentType = Constants.JSON_CONTENT_TYPE;
        context.Response.StatusCode = StatusCode;

        // Serialize and return the response
        string jsonResponse = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(jsonResponse);
    }
}
