using Grand.Business.Core.Extensions;
using Grand.Business.Core.Interfaces.Common.Configuration;
using Grand.Business.Core.Interfaces.Common.Localization;
using Grand.Infrastructure.Plugins;

namespace Authentication.OpenIdConnect
{
    /// <summary>
    /// Represents method for the authentication with external account using OpenID Connect
    /// </summary>
    public class OpenIdConnectAuthenticationPlugin : BasePlugin, IPlugin
    {
        #region Fields

        private readonly ISettingService _settingService;
        private readonly ITranslationService _translationService;
        private readonly ILanguageService _languageService;

        #endregion

        #region Ctor

        public OpenIdConnectAuthenticationPlugin(ISettingService settingService, ITranslationService translationService, ILanguageService languageService)
        {
            _settingService = settingService;
            _translationService = translationService;
            _languageService = languageService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string ConfigurationUrl()
        {
            return OpenIdAuthenticationDefaults.ConfigurationUrl;
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        public override async Task Install()
        {
            //settings
            await _settingService.SaveSetting(new OpenIdConnectExternalAuthSettings());

            //locales
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.Login", "Login using external account using OpenID Connect");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.ClientKeyIdentifier", "Client ID");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.ClientSecret", "Client Secret");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.Authority", "Authority");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.CallbackPath", "Callback Path");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.RequireHttpsMetadata", "Require Https Metadata Url");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.Title", "<h4>Configuring OIDC </h4>");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.Failed", "Failed authentication");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.Failed.Errormessage", "Error message: ");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.DisplayOrder", "Display order");

            await base.Install();
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        public override async Task Uninstall()
        {
            //settings
            await _settingService.DeleteSetting<OpenIdConnectExternalAuthSettings>();

            //locales
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.Login");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.ClientKeyIdentifier");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.ClientSecret");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.Authority");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.CallbackPath");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.RequireHttpsMetadata");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.Title");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.DisplayOrder");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.Failed");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Plugins.ExternalAuth.OIDC.Failed.Errormessage");

            await base.Uninstall();
        }

        #endregion
    }
}
