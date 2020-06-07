using Newtonsoft.Json;

namespace ProjetoApollo.Apollo.Dto
{
    public class AddressDto
    {
        [JsonProperty("cep")]
        public virtual string CEP { get; set; }

        [JsonProperty("addressLine")]
        public virtual string AddressLine { get; set; }

        [JsonProperty("number")]
        public virtual string Number { get; set; }

        [JsonProperty("complement")]
        public virtual string Complement { get; set; }

        [JsonProperty("district")]
        public virtual string District { get; set; }

        [JsonProperty("city")]
        public virtual string City { get; set; }

        [JsonProperty("state")]
        public virtual string State { get; set; }
    }
}