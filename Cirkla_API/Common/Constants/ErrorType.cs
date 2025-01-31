namespace Cirkla_API.Common.Constants;

/// <summary>
/// For use in ServiceResult to map type of error
/// </summary>
public enum ErrorType
{
    None,
    NotFound,
    ValidationError,
    Unauthorized,
    InternalError
}
