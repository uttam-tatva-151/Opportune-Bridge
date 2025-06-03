namespace Core.Beans;

public class ErrorLogDTO
{
    public string ErrorMessage { get; set; } = string.Empty; // The error message
    public string? StackTrace { get; set; } = string.Empty; // The stack trace of the exception
    public string ExceptionType { get; set; } = string.Empty; // The type of exception (e.g., NullReferenceException)
    public string StatusCode { get; set; } = null!; 
    public string? ControllerName { get; set; } 
    public string? ActionName { get; set; } 
}
