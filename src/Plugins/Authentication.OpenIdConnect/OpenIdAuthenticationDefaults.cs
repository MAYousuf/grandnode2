
namespace Authentication.OpenIdConnect
{
    /// <summary>
    /// Default values used by the Google authentication middleware
    /// </summary>
    public static class OpenIdAuthenticationDefaults
    {
        /// <summary>
        /// System name of the external authentication method
        /// </summary>
        public const string ProviderSystemName = "ExternalAuth.OpenIdConnect";

        public const string ConfigurationUrl = "../OpenIdAuthenticationSettings/Configure";
    }
}
