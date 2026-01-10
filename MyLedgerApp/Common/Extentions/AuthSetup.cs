using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyLedgerApp.Application.Services.Auth;


namespace MyLedgerApp.Common.Extentions
{
    public static class AuthSetup
    {
        /// <summary>
        /// Custom extension in <see cref="IServiceCollection"/>, to configure App's Authentication.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthSetup(this IServiceCollection services, ConfigurationManager configuration)
        {
            AuthConfig.ConfigureAuth(services, configuration);
            return services;
        }
    }
}
