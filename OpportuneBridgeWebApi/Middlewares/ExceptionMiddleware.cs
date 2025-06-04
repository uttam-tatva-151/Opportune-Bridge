using System.Net;
using System.Text.Json;
using WebApi.Utilities;
using Microsoft.AspNetCore.Mvc.Controllers;
using Core.Beans;
using Services.Interfaces;

namespace WebApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
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

    private static string GetControllerName(HttpContext context)
    {
        Endpoint? endpoint = context.GetEndpoint();
        return endpoint?.Metadata?.GetMetadata<ControllerActionDescriptor>()?.ControllerName ?? "Unknown";
    }
    private static string GetActionName(HttpContext context)
    {
        Endpoint? endpoint = context.GetEndpoint();
        return endpoint?.Metadata?.GetMetadata<ControllerActionDescriptor>()?.ActionName ?? "Unknown";
    }
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        IErrorLogService errorLogService = context.RequestServices.GetRequiredService<IErrorLogService>();
        (string ShortErrorMessage, int StatusCode) = ExceptionHelper.GetErrorShortModel(exception);

        ErrorLogDTO errorDetails = new()
        {
            ErrorMessage = exception.Message,
            StackTrace = exception.StackTrace?.Trim() ?? "No stack trace available",
            ExceptionType = exception.GetType().Name,
            ControllerName = GetControllerName(context),
            ActionName = GetActionName(context),
            StatusCode = StatusCode.ToString()
        };

        // Save error details to the error log database 
        await errorLogService.SaveErrorLogAsync(errorDetails);

        // Prepare the response
        var response = new
        {
            error = exception.Message,
            message = ShortErrorMessage,
            status = StatusCode
        };

        // Set response headers and content
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCode;

        // Serialize and return the response
        string jsonResponse = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(jsonResponse);
    }
}
