using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoApollo.Apollo.Core
{
    [Table("MedicalConsultation", Schema = "Apollo")]
    public class MedicalConsultation : Entity<int>
    {
        public virtual string Priority { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual long? ClientId { get; set; }
        public virtual Client Client { get; set; }
        public virtual long? DoctorId { get; set; }
        public virtual Doctors Doctors { get; set; }
    }
}