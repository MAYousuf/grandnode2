using Grand.Business.Core.Interfaces.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Authentication.OpenIdConnect.Infrastructure
{
    /// <summary>
    /// Registration of open id connect authentication service (plugin)
    /// </summary>
    public class OpenIdConnectAuthenticationBuilder : IAuthenticationBuilder
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="builder">Authentication builder</param>
        /// <param name="configuration">Configuration</param>
        public void AddAuthentication(AuthenticationBuilder builder, IConfiguration configuration)
        {
            builder.AddOpenIdConnect(options =>
            {
                var authority = configuration["OpenIdConnectSettings:Authority"];
                var clientId = configuration["OpenIdConnectSettings:ClientId"];
                var clientSecret = configuration["OpenIdConnectSettings:ClientSecret"];
                var callbackPath = configuration["OpenIdConnectSettings:CallbackPath"];
                var requireHttpsMetadata = configuration["OpenIdConnectSettings:RequireHttpsMetadata"];
                var metadataaddress= configuration["OpenIdConnectSettings:MetadataAddress"];
                var requirehttpsmetadata = configuration.GetValue<bool>("OpenIDConnectSettings:RequireHttpsMetadata", false);

                options.ClientId = !string.IsNullOrWhiteSpace(clientId) ? clientId : "000";
                options.ClientSecret = !string.IsNullOrWhiteSpace(clientSecret) ? clientSecret : "000";
                options.Authority = !string.IsNullOrWhiteSpace(authority) ? authority : "000";
                options.MetadataAddress = !string.IsNullOrWhiteSpace(metadataaddress) ? metadataaddress : "000";
                options.CallbackPath = !string.IsNullOrWhiteSpace(callbackPath) ? callbackPath : "/signin-oidc";
                options.RequireHttpsMetadata = requirehttpsmetadata;
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.SaveTokens = true;

                //options.Authority = "https://login.microsoftonline.com/0b596634-c24a-46c2-97fa-211ee1b0ef0c/v2.0";
                //options.ClientId = "c6a38278-5bba-49cc-b152-fff1bf9cfcd5";
                //options.ClientSecret = "{your-client-secret}";
                //options.CallbackPath = "/signin-oidc";
                ////options.ResponseType = OpenIdConnectResponseType.Code;
                //options.SaveTokens = true;
            });

        }
        public int Priority => 500;

    }
}
