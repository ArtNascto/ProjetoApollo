using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProjetoApollo.EntityFrameworkCore;
using ProjetoApollo.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ProjetoApollo.Web.Tests
{
    [DependsOn(
        typeof(ProjetoApolloWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ProjetoApolloWebTestModule : AbpModule
    {
        public ProjetoApolloWebTestModule(ProjetoApolloEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProjetoApolloWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ProjetoApolloWebMvcModule).Assembly);
        }
    }
}