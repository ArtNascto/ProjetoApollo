using Abp.AutoMapper;
using Newtonsoft.Json;
using ProjetoApollo.Apollo.Core;

namespace ProjetoApollo.Apollo.Dto
{
    [AutoMapTo(typeof(Contact))]
    
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