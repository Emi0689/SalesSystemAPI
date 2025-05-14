using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Utility;
using System.Net;

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
            int statusCode;

            if (context.Exception is AppException appEx)
            {
                statusCode = appEx.StatusCode;
                _logger.LogError(context.Exception, 
                    $"Handled Exception: ErrorCode: {appEx.ErrorCode}, " +
                    $"Title: {appEx.Title}, " +
                    $"Detail: {appEx.Detail}");
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;

                _logger.LogError(context.Exception,
                    $"Handled Exception: ErrorCode: UNHANDLED_EXCEPTION, " +
                    $"Title: Unhandled Exception, " +
                    $"Detail: default error");
            }

            var response = new Response<Exception>
            {
                Success = false,
                Value = context.Exception,
                ErrorMessage = "An unexpected error occurred."
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
