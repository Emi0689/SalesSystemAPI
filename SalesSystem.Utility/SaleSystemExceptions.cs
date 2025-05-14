using System.Net;

namespace SalesSystem.Utility
{
    public abstract class AppException : Exception
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }
        public string Title { get; }
        public string? Detail { get; }

        protected AppException(string message, int statusCode, string errorCode, string title, string? detail = null)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Title = title;
            Detail = detail;
        }
    }

    public class BadRequestException : AppException
    {
        public BadRequestException(string message, string? detail = null)
            : base(message, (int)HttpStatusCode.BadRequest, "BAD_REQUEST_RROR", "Validation failed", detail)
        {
        }
    }

    public class ArgumentNullException : AppException
    {
        public ArgumentNullException(string message, string? detail = null)
            : base(message, (int)HttpStatusCode.BadRequest, "ARGUMENT_NULL_ERROR", "Validation failed", detail)
        {
        }
    }

    public class NotFoundException : AppException
    {
        public NotFoundException(string message, string? detail = null)
            : base(message, (int)HttpStatusCode.NotFound, "NOT_FOUND_ERROR", "Validation failed", detail)
        {
        }
    }

    public class ValidationException : AppException
    {
        public ValidationException(string message, string? detail = null)
            : base(message, 400, "VALIDATION_ERROR", "Validation failed", detail)
        {
        }
    }

    public class UnhandledException : AppException
    {
        public UnhandledException(string message, string? detail = null)
            : base(message, (int)HttpStatusCode.InternalServerError, "UNHANDLED_EXCEPTION", "Unhandled Exception", detail)
        {
        }
    }
}
