using Grand.Infrastructure.ModelBinding;
using Grand.Infrastructure.Models;

namespace Authentication.OpenIdConnect.Models
{
    public class ConfigurationModel : BaseModel
    {
        [GrandResourceDisplayName("Plugins.ExternalAuth.OIDC.ClientKeyIdentifier")]
        public string ClientKeyIdentifier { get; set; }
        [GrandResourceDisplayName("Plugins.ExternalAuth.OIDC.ClientSecret")]
        public string ClientSecret { get; set; }
        [GrandResourceDisplayName("Plugins.ExternalAuth.OIDC.Authority")]
        public string Authority { get; set; }
        [GrandResourceDisplayName("Plugins.ExternalAuth.OIDC.MetadataAddress")]
        public string MetadataAddress { get; set; }
        [GrandResourceDisplayName("Plugins.ExternalAuth.OIDC.CallbackPath")]
        public string CallbackPath { get; set; }
        [GrandResourceDisplayName("Plugins.ExternalAuth.OIDC.RequireHttpsMetadata")]
        public bool RequireHttpsMetadata { get; set; }
        [GrandResourceDisplayName("Plugins.ExternalAuth.OIDC.DisplayOrder")]
        public int DisplayOrder { get; set; }
    }
}
