using System.Threading.Tasks;
using InspiringQuotes.Common.AppResponse;
using InspiringQuotes.Common.CustomExceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace InpiringQuotes.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger, RequestDelegate next)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) 
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            GenericResponse<string> response = null;
            if (ex is UserFriendlyException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                response = AppResponseFactory.BadRequest(ex.Message);
            }
            else if (ex is ForbiddenException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                response = AppResponseFactory.ForbiddenError(ex.Message);
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response = AppResponseFactory.InternalError(ex.Message);
            }
            return httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}
