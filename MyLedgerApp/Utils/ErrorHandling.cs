using Microsoft.AspNetCore.Mvc;

namespace MyLedgerApp.Utils
{
    public class ErrorHandling
    {
        public static ObjectResult CreateUnexpectedError(Exception e)
        {
            var result = new
            {
                message = "Some unexpected error has occurred.",
                desc = e.Message
            };

            return new ObjectResult(result) { StatusCode = 500 };
        }
        public static ObjectResult CreateNotFoundError(Exception e)
        {
            var result = new
            {
                message = "Resource not found.",
                desc = e.Message
            };

            return new ObjectResult(result) { StatusCode = 404 };
        }
    }
}
