using Abp.AutoMapper;
using Abp.MailKit;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProjetoApollo.Authorization;
using Abp.Configuration.Startup;
using ProjetoApollo.Email;
using Abp.Dependency;
using ProjetoApollo.Apollo.Core;
using ProjetoApollo.Apollo.Dto;

namespace ProjetoApollo
{
    [DependsOn(
        typeof(ProjetoApolloCoreModule), 
        typeof(AbpAutoMapperModule), typeof(AbpMailKitModule))]
    public class ProjetoApolloApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ProjetoApolloAuthorizationProvider>();
            Configuration.ReplaceService<IMailKitSmtpBuilder, SmtpBuilder>(DependencyLifeStyle.Transient);

        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ProjetoApolloApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
                
            );
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
        {
            config.CreateMap<QuestionaryDto, Questionary>();
            config.CreateMap<ClientDto, Client>();
        });
        }
    }
}
