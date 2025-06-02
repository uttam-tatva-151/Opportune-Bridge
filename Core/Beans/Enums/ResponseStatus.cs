namespace Core.Beans.Enums;

public enum ResponseStatus
{
    Success,     // Operation completed successfully
    Failed,      // Operation failed
    Warning,     // Operation completed with a warning
    NotFound,    // Resource not found
    Unauthorized,// Operation not authorized
    ValidationError // Input validation failed
}    