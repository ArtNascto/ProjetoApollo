using System.ComponentModel.DataAnnotations;

namespace ProjetoApollo.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}