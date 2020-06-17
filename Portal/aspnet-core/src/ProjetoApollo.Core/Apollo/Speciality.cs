using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoApollo.Apollo.Core
{
    [Table("Speciality", Schema = "Apollo")]
    public class Speciality : Entity<int>
    {
        public virtual string Name { get; set; }
        public virtual int Code { get; set; }
        public virtual long InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }
    }
}