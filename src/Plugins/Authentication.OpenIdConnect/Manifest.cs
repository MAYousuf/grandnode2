using Authentication.OpenIdConnect;
using Grand.Infrastructure;
using Grand.Infrastructure.Plugins;

[assembly: PluginInfo(
    FriendlyName = "OpenIdConnect authentication",
    Group = "Authentication methods",
    SystemName = OpenIdAuthenticationDefaults.ProviderSystemName,
    SupportedVersion = GrandVersion.SupportedPluginVersion,
    Author = "Mohamed Yousuf",
    Version = "0.0.1"
)]