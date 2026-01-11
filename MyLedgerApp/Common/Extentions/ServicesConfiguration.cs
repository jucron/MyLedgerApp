using System.Reflection;
using Microsoft.OpenApi;
using MyLedgerApp.Application.Services.Auth;


namespace MyLedgerApp.Common.Extentions
{
    public static class ServicesConfiguration
    {
        /// <summary>
        /// Custom extension in <see cref="IServiceCollection"/>, to configure App's Authentication Configuration.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthConfig(this IServiceCollection services, ConfigurationManager configuration)
        {
            AuthConfig.ConfigureAuth(services, configuration);
            return services;
        }

        /// <summary>
        /// Custom extension in <see cref="IServiceCollection"/>, to configure App's Swagger Configuration.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSwaggerGen(c =>
            {
                var version = configuration["App:ApiVersion"] ?? "v1";
                c.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = "My Ledger Application",
                    Version = version,
                    Description = "A Ledger Application to manage your transactions."
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}
