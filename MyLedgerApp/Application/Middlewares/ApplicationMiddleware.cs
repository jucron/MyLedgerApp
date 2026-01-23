using System.Text.Json;
using MyLedgerApp.Application.Handlers;
using MyLedgerApp.Common.Utils;

namespace MyLedgerApp.Application.Middlewares
{
    public class ApplicationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                SetCancellationToken(context);
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            finally
            {
                ClearCancellationToken();
            }
        }

        private static void ClearCancellationToken()
        {
            ReqCanToken.Clear();
        }

        private static void SetCancellationToken(HttpContext context)
        {
            ReqCanToken.Set(context.RequestAborted);
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
