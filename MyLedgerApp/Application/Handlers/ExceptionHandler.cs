using System.Net;
using System.Text.Json;
using FluentValidation;

namespace MyLedgerApp.Application.Handlers
{
    public static class ExceptionHandler
    {
        public static (HttpStatusCode statusCode, string message) HandleException(Exception ex)
        {
            return ex switch
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
        }
    }
}
