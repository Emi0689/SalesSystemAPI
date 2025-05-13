using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Utility;
using System.ComponentModel.DataAnnotations;

namespace SalesSystem.API.Common
{
    public class HttpResponseExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpResponseExceptionFilter> _logger;

        public HttpResponseExceptionFilter(ILogger<HttpResponseExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Handled Exception.");

            var response = new
            {
                message = context.Exception.Message,
                exceptionType = context.Exception.GetType().Name
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = context.Exception switch
                {
                    NotFoundException => 404,
                    ConflictException => 409,
                    ValidationException => 400,
                    _ => 500
                }
            };

            context.ExceptionHandled = true;
        }
    }
}
