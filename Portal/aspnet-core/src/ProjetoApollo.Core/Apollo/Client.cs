using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ProjetoApollo.Apollo.Core {
    [Table ("Client", Schema = "Apollo")]
    public class Client : Entity<long> {
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Uin { get; set; }
        public virtual Address Address { get; set; }

        public virtual List<Historic> Historic { get; set; }
        public virtual List<QuestionaryAnswers> QuestionaryAnswers { get; set; }
        public virtual List<MedicalConsultation> MedicalConsultation { get; set; }
    }
}