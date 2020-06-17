using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ProjetoApollo.Apollo.Core {
    [Table ("QuestionaryAnswers", Schema = "Apollo")]
    public class QuestionaryAnswers : Entity<long> {
        public virtual int QuestionId { get; set; }
        public virtual Questionary Questionary { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual long? ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}