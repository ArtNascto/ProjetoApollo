using Newtonsoft.Json;

namespace ProjetoApollo.Apollo.Dto
{
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