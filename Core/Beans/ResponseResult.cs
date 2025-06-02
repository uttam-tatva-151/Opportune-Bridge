using Core.Beans.Enums;

namespace Core.Beans;

public class ResponseResult<T>
{
    public ResponseStatus Status { get; set; }           // Indicates if the operation was successful
    public string Message { get; set; } = null!;        // A message describing the result
    public T? Data { get; set; }  
}
