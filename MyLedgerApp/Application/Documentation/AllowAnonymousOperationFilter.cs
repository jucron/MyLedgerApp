using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyLedgerApp.Application.Documentation
{
    /// <summary>
    /// Apply a filter to show in Swagger the resource as "open" for inquiries.
    /// </summary>
    public class AllowAnonymousOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAllowAnonymous =
                context.MethodInfo.GetCustomAttributes(true)
                    .OfType<AllowAnonymousAttribute>().Any();

            if (hasAllowAnonymous)
                operation.Security = [];
        }
    }

}
