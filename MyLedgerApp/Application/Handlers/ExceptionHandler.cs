using System.Net;
using System.Text.Json;
using FluentValidation;
using static MyLedgerApp.Common.Utils.Exceptions;

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
                ResourceNotFoundException re => (
                    HttpStatusCode.NotFound,
                    re.Message
                ),
                InvalidOperationException io => (
                    HttpStatusCode.Forbidden, 
                    io.Message
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
