using Abp.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.ComTypes;

namespace ProjetoApollo.Apollo.Core
{
[Table("Institution", Schema="Apollo")]
    public class Institution : Entity<long>
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public virtual long Id {get;set;}
        public virtual string Name { get; set; }

        public virtual string CNPJ { get; set; }

        public virtual List<Address> Addresses { get; set; }

        public virtual Contact TechnicalContact { get; set; }

        public virtual Billing BillingInfo { get; set; }

    }
}
