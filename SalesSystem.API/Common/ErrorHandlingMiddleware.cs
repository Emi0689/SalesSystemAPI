using SalesSystem.Utility;
using System.Net;
using System.Text.Json;

namespace SalesSystem.API.Common
{
    public class ErrorHandlingMiddleware
    {
        //This class is for unhandler-exceptions

        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code;
            string error = string.Empty;

            switch (exception)
            {
                case ArgumentNullException _:
                    code = HttpStatusCode.BadRequest;
                    error = "A required argument was null.";
                    break;
                case UnauthorizedAccessException _:
                    code = HttpStatusCode.Unauthorized;
                    error = "Unauthorized access.";
                    break;
                case BadRequestException _:
                    code = HttpStatusCode.BadRequest;
                    error = "Bad Request.";
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    error = "An unexpected error occurred.";
                    break;
            }

            var response = new Response<Exception>();
            response.Success = false;
            response.Value = exception;
            response.ErrorMessage = error;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
