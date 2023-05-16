using Authentication.OpenIdConnect.Models;
using Grand.Business.Core.Interfaces.Authentication;
using Grand.SharedKernel;
using Grand.Web.Common.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Grand.Business.Core.Utilities.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Authentication.OpenIdConnect.Controllers
{
    public class OpenIdAuthenticationController : BasePluginController
    {
        #region Fields

        private readonly IExternalAuthenticationService _externalAuthenticationService;
        private readonly IConfiguration _configuration;

        #endregion

        #region Ctor

        public OpenIdAuthenticationController(IExternalAuthenticationService externalAuthenticationService,
            IConfiguration configuration)
        {
            _externalAuthenticationService = externalAuthenticationService;
            _configuration = configuration;
        }

        #endregion

        #region Methods

        public IActionResult OpenIdLogin(string returnUrl)
        {
            if (!_externalAuthenticationService.AuthenticationProviderIsAvailable(OpenIdAuthenticationDefaults.ProviderSystemName))
                throw new GrandException("OpenID Connect authentication module cannot be loaded");

            if (string.IsNullOrEmpty(_configuration["OpenIDConnectSettings:ClientId"]) || string.IsNullOrEmpty(_configuration["OpenIDConnectSettings:ClientSecret"]))
                throw new GrandException("OpenID Connect authentication module not configured");

            //configure login callback action
            var authenticationProperties = new AuthenticationProperties {
                RedirectUri = Url.Action("OpenIdLoginCallback", "OpenIdAuthentication", new { returnUrl })
            };

            return Challenge(authenticationProperties, OpenIdConnectDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OpenIdLoginCallback(string returnUrl)
        {
            //authenticate external user
            var authenticateResult = await HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded || !authenticateResult.Principal.Claims.Any())
                return RedirectToRoute("Login");

            //create external authentication parameters
            var authenticationParameters = new ExternalAuthParam {
                ProviderSystemName = OpenIdAuthenticationDefaults.ProviderSystemName,
                AccessToken = await HttpContext.GetTokenAsync(OpenIdConnectDefaults.AuthenticationScheme, "access_token"),
                Email = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email)?.Value,
                Identifier = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value,
                Name = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name)?.Value,
                Claims = authenticateResult.Principal.Claims.ToList()
            };

            //authenticate grand user
            return await _externalAuthenticationService.Authenticate(authenticationParameters, returnUrl);
        }

        public IActionResult OpenIdSignInFailed(string error_message)
        {
            //handle exception and display message to user
            var model = new FailedModel() {
                ErrorMessage = error_message
            };
            return View(model);
        }
        #endregion
    }
}