using System.Threading.Tasks;
using Abp.Application.Services;
using ProjetoApollo.Apollo.Dto;
using ProjetoApollo.Apollo.Dto.Input;
using ProjetoApollo.Apollo.Dto.Output;

namespace ProjetoApollo.Apollo
{
    public interface IApolloAppService : IApplicationService
    {
        Task<CreateInstitutionOutput> RegisterInstitution(CreateInstitutionInput input);
        InstitutionDto CreateInstitution ();
    }
}