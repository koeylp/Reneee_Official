using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Reneee.Application.Exceptions;
using System.Net;

namespace Reneee.API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode.ToString() == "401")
                {
                    httpContext.Response.ContentType = "application/json";
                    var response = new { error = "Unauthorized access" };
                    await httpContext.Response.WriteAsJsonAsync(response);
                }
                else if (httpContext.Response.StatusCode.ToString() == "403")
                {
                    httpContext.Response.ContentType = "application/json";
                    var response = new { error = "Access Denied" };
                    await httpContext.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string result = JsonConvert.SerializeObject(new ErrorDetails
            {
                ErrorMessage = exception.Message,
                ErrorType = "Failure"
            });

            switch (exception)
            {
                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.Errors);
                    break;
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case UnauthorizedException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case ForbiddenException forbiddenException:
                    statusCode = HttpStatusCode.Forbidden;
                    result = JsonConvert.SerializeObject(new ErrorDetails
                    {
                        ErrorType = "Forbidden",
                        ErrorMessage = forbiddenException.Message
                    });
                    break;
                case SecurityTokenExpiredException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                default:
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }

    public class ErrorDetails
    {
        public string? ErrorType { get; set; }
        public string? ErrorMessage { get; set; }
    }

}
