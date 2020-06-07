using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProjetoApollo.Authorization;

namespace ProjetoApollo
{
    [DependsOn(
        typeof(ProjetoApolloCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ProjetoApolloApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ProjetoApolloAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ProjetoApolloApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
