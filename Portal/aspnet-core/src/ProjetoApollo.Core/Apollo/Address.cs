using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ProjetoApollo.Apollo.Core {
    [Table ("Address", Schema = "Apollo")]
    public class Address : Entity<long> {
        public virtual string CEP { get; set; }

        public virtual string AddressLine { get; set; }

        public virtual string Number { get; set; }

        public virtual string Complement { get; set; }

        public virtual string District { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual long? InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }
        public virtual long? ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}