using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoApollo.Apollo.Core
{
    [Table("Contact", Schema = "Apollo")]
    public class Contact : Entity<int>
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string ContactNumber { get; set; }

        public virtual long InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }

    }
}
