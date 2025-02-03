using Cirkla_API.Common.Constants;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cirkla_API.Common;

public class ServiceResult<T>
{
    public bool IsError { get; set; }
    public ErrorType Error { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Payload { get; set; }

    private ServiceResult(bool isError, ErrorType error, string? errorMessage, T? payload)
    {
        IsError = isError;
        Error = error;
        ErrorMessage = errorMessage;
        Payload = payload;
    }

    public static ServiceResult<T> Success(T payload) => new(false, default,null, payload);
    public static ServiceResult<T> Fail(string errorMessage, ErrorType error) => new(true, error, errorMessage, default);


}