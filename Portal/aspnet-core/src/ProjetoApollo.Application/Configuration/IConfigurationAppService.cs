using System.Threading.Tasks;
using ProjetoApollo.Configuration.Dto;

namespace ProjetoApollo.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
