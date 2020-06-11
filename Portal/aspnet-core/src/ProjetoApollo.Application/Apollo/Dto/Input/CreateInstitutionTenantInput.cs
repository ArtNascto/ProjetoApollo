using Newtonsoft.Json;

namespace ProjetoApollo.Apollo.Dto.Input
{
    public class CreateInstitutionTenantInput
    {
       public virtual string TenancyName {get;set;}
       public virtual string InstitutionName {get;set;}
       public virtual string adminEmail {get;set;}
    }
}