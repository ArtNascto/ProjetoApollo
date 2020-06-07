using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProjetoApollo.Configuration;

namespace ProjetoApollo.Web.Host.Startup
{
    [DependsOn(
       typeof(ProjetoApolloWebCoreModule))]
    public class ProjetoApolloWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ProjetoApolloWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProjetoApolloWebHostModule).GetAssembly());
        }
    }
}
