using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoApollo.Apollo.Core
{
    [Table("Doctors", Schema = "Apollo")]
    public class Doctors : Entity<long>
    {
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string CRM { get; set; }
        public virtual int SpecialityId { get; set; }
        public virtual Speciality Speciality { get; set; }

        public virtual long InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }
    }
}