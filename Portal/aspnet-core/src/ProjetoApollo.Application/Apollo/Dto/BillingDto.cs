using Abp.AutoMapper;
using Newtonsoft.Json;
using ProjetoApollo.Apollo.Core;

namespace ProjetoApollo.Apollo.Dto
{
    [AutoMapTo(typeof(Billing))]

    public class BillingDto
    {
        [JsonProperty("accountHolderName")]
        public virtual string AccountHolderName { get; set; }

        [JsonProperty("accountNumber")]
        public virtual string AccountNumber { get; set; }

        [JsonProperty("expiresDate")]
        public virtual string ExpiresDate { get; set; }
    }
}