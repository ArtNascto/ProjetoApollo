using System.Collections.Generic;
using Abp.AutoMapper;
using Newtonsoft.Json;
using ProjetoApollo.Apollo.Core;

namespace ProjetoApollo.Apollo.Dto
{
    [AutoMapTo(typeof(Institution))]
    
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

        [JsonProperty("billingInfo")]
        public virtual BillingDto BillingInfo { get; set; }
    }
}