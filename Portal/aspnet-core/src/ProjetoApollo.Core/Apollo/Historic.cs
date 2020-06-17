using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoApollo.Apollo.Core
{
    [Table("Historic", Schema = "Apollo")]
    public class Historic : Entity<long>
    {
        public virtual string Status { get; set; }
        public virtual string Type { get; set; }
        public virtual string Description { get; set; }
        public virtual long? ClientId { get; set; }
        public virtual Client Client { get; set; }
        public virtual long? DoctorId { get; set; }
        public virtual Doctors Doctors { get; set; }
    }
}