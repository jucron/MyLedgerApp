using System.Net;
using System.Text.Json;
using FluentValidation;

namespace MyLedgerApp.Application.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }


            try
            {
                await _next(context); // pass control to next middleware / controller
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, message) = exception switch
            {
                ValidationException ve => (
                    HttpStatusCode.BadRequest,
                    JsonSerializer.Serialize(ve.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }))
                ),
                UnauthorizedAccessException ua => (
                    HttpStatusCode.Unauthorized,
                    ua.Message
                ),
                ArgumentException ae => (
                    HttpStatusCode.BadRequest,
                    ae.Message
                ),
                _ => (
                    HttpStatusCode.InternalServerError,
                    "An unexpected error occurred."
                )
            };

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                error = message,
                exceptionType = exception.GetType().Name
            }));

        }
    }
}
