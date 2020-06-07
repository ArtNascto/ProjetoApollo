using Newtonsoft.Json;

namespace ProjetoApollo.Apollo.Dto
{
    public class ContactDto
    {
        [JsonProperty("name")]
        public virtual string Name { get; set; }
        
        [JsonProperty("email")]
        public virtual string Email { get; set; }
        
        [JsonProperty("contactNumber")]
        public virtual string ContactNumber { get; set; }
    }
}