using Abp.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoApollo.Apollo.Core
{
    [Table("Institution", Schema = "Apollo")]
    public class Institution : Entity<long>
    {
        public virtual string Name { get; set; }

        public virtual string CNPJ { get; set; }

        public virtual List<Doctors> Doctors { get; set; }
        public virtual List<Speciality> Speciality { get; set; }

        public virtual List<Address> Addresses { get; set; }

        public virtual Contact TechnicalContact { get; set; }

        public virtual Billing BillingInfo { get; set; }
    }
}