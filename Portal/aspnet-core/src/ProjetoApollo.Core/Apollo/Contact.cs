using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ProjetoApollo.Apollo.Core {
    [Table ("Contact", Schema = "Apollo")]
    public class Contact : Entity<int> {
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string ContactNumber { get; set; }

        public virtual long InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }
    }
}