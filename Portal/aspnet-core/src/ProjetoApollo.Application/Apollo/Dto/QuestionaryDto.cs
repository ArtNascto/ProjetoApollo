using Abp.AutoMapper;
using ProjetoApollo.Apollo.Core;

namespace ProjetoApollo.Apollo.Dto
{
    [AutoMapTo(typeof(Questionary))]
    public class QuestionaryDto
    {
        public virtual int Id { get; set; }
        public virtual string Label { get; set; }
        public virtual string Description { get; set; }
        public virtual string Priority { get; set; }
        public virtual int NextQuestion { get; set; }
    }
}