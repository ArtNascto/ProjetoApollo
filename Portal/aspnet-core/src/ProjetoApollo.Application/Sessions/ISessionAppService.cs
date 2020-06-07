using System.Threading.Tasks;
using Abp.Application.Services;
using ProjetoApollo.Sessions.Dto;

namespace ProjetoApollo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
