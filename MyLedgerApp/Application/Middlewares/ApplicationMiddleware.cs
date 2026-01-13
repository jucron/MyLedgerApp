using System.Text.Json;
using MyLedgerApp.Application.Handlers;

namespace MyLedgerApp.Application.Middlewares
{
    public class ApplicationMiddleware
    {
        private readonly RequestDelegate _next;

        public ApplicationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // pass control to next middleware / controller
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            var (statusCode, message) = ExceptionHandler.HandleException(exception);

            context.Response.StatusCode = (int)statusCode;
            return context.Response
                .WriteAsync(JsonSerializer.Serialize(new
                {
                    error = message,
                    exceptionType = exception.GetType().Name
                }));

        }
    }
}
