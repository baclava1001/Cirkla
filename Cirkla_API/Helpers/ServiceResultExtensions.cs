using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Helpers;

public static class ServiceResultExtensions
{
    public static IActionResult ToHttpResponse<T>(this ServiceResult<T> result)
    {
        if (result.IsCreated)
            return new CreatedResult("", result.Payload);
        if (!result.IsError)
            return new OkObjectResult(result.Payload);

        var problemDetails = new ProblemDetails
        {
            Title = result.Error.ToString(),
            Detail = result.ErrorMessage,
            Status = GetStatusCode(result.Error)
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }

    private static int GetStatusCode(ErrorType? errorType)
    {
        return errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.ValidationError => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.InternalError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
