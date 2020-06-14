using Abp.Configuration;
using System.Collections.Generic;

namespace ProjetoApollo.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(AppSettingNames.EmailUsername, string.Empty, scopes: SettingScopes.Application, isVisibleToClients: false),
                new SettingDefinition(AppSettingNames.EmailPassword,  string.Empty, scopes: SettingScopes.Application, isVisibleToClients: false),
                new SettingDefinition(AppSettingNames.EmailUseSSL,  "false", scopes: SettingScopes.Application, isVisibleToClients: false),
                new SettingDefinition(AppSettingNames.EmailSMTPServer,  "smtp.gmail.com", scopes: SettingScopes.Application, isVisibleToClients: false),
                new SettingDefinition(AppSettingNames.EmailSMTPPort, "587", scopes: SettingScopes.Application, isVisibleToClients: false),
            };
        }
    }
}