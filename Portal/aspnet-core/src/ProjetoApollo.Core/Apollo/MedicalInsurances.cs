
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ProjetoApollo.Apollo.Core {
    [Table ("MedicalInsurances", Schema = "Apollo")]
    public class MedicalInsurances : Entity<long> {
        public virtual string Name { get; set; }
        
    }
}