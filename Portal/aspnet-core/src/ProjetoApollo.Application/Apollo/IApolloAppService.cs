using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using ProjetoApollo.Apollo.Dto;
using ProjetoApollo.Apollo.Dto.Input;
using ProjetoApollo.Apollo.Dto.Output;
using ProjetoApollo.Authorization.Accounts.Dto;

namespace ProjetoApollo.Apollo.AppService
{
    public interface IApolloAppService : IApplicationService
    {
        Task<CreateInstitutionOutput> RegisterInstitution(CreateInstitutionInput input);
        InstitutionDto CreateInstitution ();
        Task<RegisterOutput> RegisterClient(RegisterInput input);
        Task<QuestionaryDto> GetQuestionary();
        Task<List<string>> GetMedicalInsurances();
        Task<ClientDto> GetClient ();
    }
}