using Abp.AutoMapper;
using Newtonsoft.Json;
using ProjetoApollo.Apollo.Core;

namespace ProjetoApollo.Apollo.Dto {
    [AutoMapTo (typeof (Client))]

    public class ClientDto {
        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual string MedicalInsurance { get; set; }

        public virtual long UserId { get; set; }

    }
}