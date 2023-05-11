using Authentication.OpenIdConnect.Models;
using Grand.Business.Core.Interfaces.Common.Configuration;
using Grand.Business.Core.Interfaces.Common.Localization;
using Grand.Business.Core.Interfaces.Common.Security;
using Grand.Business.Core.Utilities.Common.Security;
using Grand.Web.Common.Controllers;
using Grand.Web.Common.Filters;
using Grand.Web.Common.Security.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Authentication.OpenIdConnect.Controllers
{
    [AuthorizeAdmin]
    [Area("Admin")]
    [PermissionAuthorize(PermissionSystemName.ExternalAuthenticationMethods)]
    public class OpenIdAuthenticationSettingsController : BasePluginController
    {
        #region Fields

        private readonly OpenIdConnectExternalAuthSettings _oidcExternalAuthSettings;
        private readonly ITranslationService _translationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IConfiguration _configuration;

        #endregion

        #region Ctor

        public OpenIdAuthenticationSettingsController(
            OpenIdConnectExternalAuthSettings oidcExternalAuthSettings,
            ITranslationService translationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IConfiguration configuration)
        {
            _oidcExternalAuthSettings = oidcExternalAuthSettings;
            _translationService = translationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _configuration = configuration;
        }

        #endregion

        #region Methods


        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.Authorize(StandardPermission.ManageExternalAuthenticationMethods))
                return AccessDeniedView();

            var model = new ConfigurationModel {
                ClientKeyIdentifier = _configuration["OpenIDConnectSettings:ClientId"],
                ClientSecret = _configuration["OpenIDConnectSettings:ClientSecret"],
                Authority = _configuration["OpenIDConnectSettings:Authority"],
                CallbackPath = _configuration["OpenIDConnectSettings:CallbackPath"],
                RequireHttpsMetadata = false,
                DisplayOrder = _oidcExternalAuthSettings.DisplayOrder
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.Authorize(StandardPermission.ManageExternalAuthenticationMethods))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return await Configure();

            _oidcExternalAuthSettings.DisplayOrder = model.DisplayOrder;

            await _settingService.SaveSetting(_oidcExternalAuthSettings);

            //now clear settings cache
            await _settingService.ClearCache();

            Success(_translationService.GetResource("Admin.Plugins.Saved"));

            return await Configure();

        }

        #endregion
    }
}