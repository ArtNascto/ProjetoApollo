using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProjetoApollo.Apollo.Dto
{
    public class InstitutionDto
    {
        [JsonProperty("name")]
        public virtual string Name { get; set; }

        [JsonProperty("cnpj")]
        public virtual string CNPJ { get; set; }

        [JsonProperty("addresses")]
        public virtual List<AddressDto> Addresses { get; set; }

        [JsonProperty("technicalContact")]
        public virtual ContactDto TechnicalContact { get; set; }

        [JsonProperty("bussinessContact")]
        public virtual ContactDto BussinessContact { get; set; }

        [JsonProperty("billingInfo")]
        public virtual BillingDto BillingInfo { get; set; }
    }
}