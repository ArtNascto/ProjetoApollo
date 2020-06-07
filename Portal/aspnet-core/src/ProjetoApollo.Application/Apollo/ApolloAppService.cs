using System.Collections.Generic;
using System.Threading.Tasks;
using ProjetoApollo.Apollo.Dto;
using ProjetoApollo.Apollo.Dto.Input;
using ProjetoApollo.Apollo.Dto.Output;


namespace ProjetoApollo.Apollo
{
    public class ApolloAppService : IApolloAppService
    {

        public async Task<CreateInstitutionOutput> RegisterInstitution(CreateInstitutionInput input)
        {
            var output = new CreateInstitutionOutput();

            return output;
        }
        public InstitutionDto CreateInstitution()
        {
            var output = new InstitutionDto();
            var emptyAddress = new AddressDto()
            {
                CEP = string.Empty,
                AddressLine = string.Empty,
                Number = string.Empty,
                Complement = string.Empty,
                District = string.Empty,
                City = string.Empty,
                State = string.Empty
            };
            var emptyListAddress = new List<AddressDto>();
            emptyListAddress.Add(emptyAddress);
            output.Addresses = emptyListAddress;
            output.BussinessContact = new ContactDto();
            output.TechnicalContact = new ContactDto();
            output.BillingInfo = new BillingDto();
            return output;
        }
    }
}