using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoApollo.Apollo.Core
{
    [Table("Questionary", Schema = "Apollo")]
    public class Questionary : Entity<int>
    {
        public virtual string Label { get; set; }
        public virtual string Description { get; set; }
        public virtual string Priority { get; set; }
        public virtual int NextQuestion { get; set; }
        public virtual string Type { get; set; }
    }
}