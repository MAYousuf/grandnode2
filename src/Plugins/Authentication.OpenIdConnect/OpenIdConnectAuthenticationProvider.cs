using Grand.Business.Core.Interfaces.Authentication;

namespace Authentication.OpenIdConnect
{
    public class OpenIdConnectAuthenticationProvider : IExternalAuthenticationProvider
    {
        private readonly OpenIdConnectExternalAuthSettings _oidcExternalAuthSettings;

        public OpenIdConnectAuthenticationProvider(OpenIdConnectExternalAuthSettings oidcExternalAuthSettings)
        {
            _oidcExternalAuthSettings = oidcExternalAuthSettings;
        }

        public string ConfigurationUrl => OpenIdAuthenticationDefaults.ConfigurationUrl;

        public string SystemName => OpenIdAuthenticationDefaults.ProviderSystemName;

        public string FriendlyName => "OpenIdConnect authentication";

        public int Priority => _oidcExternalAuthSettings.DisplayOrder;

        public IList<string> LimitedToStores => new List<string>();

        public IList<string> LimitedToGroups => new List<string>();

        /// <summary>
        /// Gets a view component for displaying plugin in public store
        /// </summary>
        public async Task<string> GetPublicViewComponentName()
        {
            return await Task.FromResult("OpenIdAuthentication");
        }

    }
}
