using Core.Beans.Enums;

namespace Core.Beans;

public class ResponseResult<T>
{
    public ResponseStatus Status { get; set; }           // Indicates if the operation was successful
    public string Message { get; set; } = null!;        // A message describing the result
    public T? Data { get; set; } 
    public List<string>? Errors { get; set; }

    public static ResponseResult<T> Success(T data, string message, ResponseStatus status)
    {
        return new ResponseResult<T>
        {
            Status = status,
            Message = message,
            Data = data
        };
    }
    public static ResponseResult<T> Failure(string message,ResponseStatus status, List<string>? errors = null)
    {
        return new ResponseResult<T>
        {
            Status = status,
            Message = message,
            Errors = errors
        };
    }
}
