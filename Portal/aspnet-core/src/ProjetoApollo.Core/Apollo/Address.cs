using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoApollo.Apollo.Core
{
    public class Address : Entity<long>
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public virtual long Id { get; set; }
        public virtual string CEP { get; set; }

        public virtual string AddressLine { get; set; }

        public virtual string Number { get; set; }

        public virtual string Complement { get; set; }

        public virtual string District { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual long InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }

    }
}
