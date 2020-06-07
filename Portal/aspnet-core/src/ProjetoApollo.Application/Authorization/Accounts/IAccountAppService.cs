using System.Threading.Tasks;
using Abp.Application.Services;
using ProjetoApollo.Authorization.Accounts.Dto;

namespace ProjetoApollo.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
