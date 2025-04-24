using System.Text.Json.Nodes;
using Cirkla_API.Common.Constants;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cirkla_API.Common;

public class ServiceResult<T>
{
    public bool IsError { get; set; }
    public ErrorType Error { get; set; } = ErrorType.None;
    public string? ErrorMessage { get; set; }
    public T? Payload { get; set; }
    public bool IsCreated { get; set; }

    private ServiceResult(bool isError, ErrorType error, string? errorMessage, T? payload, bool isCreated)
    {
        IsError = isError;
        Error = error;
        ErrorMessage = errorMessage;
        Payload = payload;
        IsCreated = isCreated;
    }


    public static ServiceResult<T> Created(T payload) => new(false, default, null, payload, true);
    public static ServiceResult<T> Success(T payload) => new(false, default,null, payload, false);
    //public static ServiceResult<object> NoContent() => new(false,default,null, new object(), false);
    public static ServiceResult<T> Fail(string errorMessage, ErrorType error) => new(true, error, errorMessage, default, false);
}