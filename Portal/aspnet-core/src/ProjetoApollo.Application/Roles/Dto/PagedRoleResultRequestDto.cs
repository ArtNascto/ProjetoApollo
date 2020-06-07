using Abp.Application.Services.Dto;

namespace ProjetoApollo.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

