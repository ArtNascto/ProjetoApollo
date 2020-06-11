using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoApollo.Apollo.Core
{
    [Table("Billing", Schema = "Apollo")]
    public class Billing : Entity<int>
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public virtual int Id { get; set; }

        public virtual string AccountHolderName { get; set; }

        public virtual string AccountNumber { get; set; }

        public virtual string ExpiresDate { get; set; }
        public virtual long InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }
    }
}
