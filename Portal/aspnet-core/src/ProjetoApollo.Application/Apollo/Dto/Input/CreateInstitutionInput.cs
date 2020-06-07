using Newtonsoft.Json;

namespace ProjetoApollo.Apollo.Dto.Input
{
    public class CreateInstitutionInput
    {
        [JsonProperty("institution")]
        public InstitutionDto Institution { get; set; }
    }
}