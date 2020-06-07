using Abp.Application.Services;
using ProjetoApollo.MultiTenancy.Dto;

namespace ProjetoApollo.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

