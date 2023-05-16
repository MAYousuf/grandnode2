using Grand.Infrastructure.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Authentication.OpenIdConnect
{
    public class EndpointProvider : IEndpointProvider
    {
        public void RegisterEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute("Plugin.ExternalAuth.OpenIdConnect.SignInOIDC",
                 "oidc-signin-failed",
                 new { controller = "OpenIdAuthentication", action = "OpenIdSignInFailed" }
            );
        }
        public int Priority => 10;
    }
}
