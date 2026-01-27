using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyLedgerApp.Application.Documentation
{
    public class SwaggerConfig
    {
        public static void ConfigSwaggerOptions(string apiVersion, SwaggerGenOptions c)
        {
            c.SwaggerDoc(apiVersion, new OpenApiInfo
            {
                Title = "My Ledger Application",
                Version = apiVersion,
                Description = "A Ledger Application to manage your transactions."
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your JWT token like: Bearer {your_token}"
            });

            c.AddSecurityRequirement(document => new() { [new OpenApiSecuritySchemeReference("Bearer", document)] = [] });

            c.OperationFilter<AllowAnonymousOperationFilter>();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        }
    }
}
