using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ProjetoApollo.Configuration.Dto;

namespace ProjetoApollo.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ProjetoApolloAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
