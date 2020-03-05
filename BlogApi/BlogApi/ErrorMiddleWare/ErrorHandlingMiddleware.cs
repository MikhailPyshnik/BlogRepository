using Microsoft.AspNetCore.Http;
using Models.Exeptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BlogApi.ErrorMiddleWare
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async  Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var currentContext = context.Response;
            var currentstatusCode = (int)HttpStatusCode.InternalServerError;
            var customException = exception as GeneralException;
            var message = exception.Message;

            if (customException != null)
            {
                message = customException.Message;
                currentstatusCode = customException.StatusCode;
            }

            await currentContext.WriteAsync(new ErrorDetails()
            {
                StatusCode = currentstatusCode,
                Message = message
            }.ToString());
        }
    }
}
