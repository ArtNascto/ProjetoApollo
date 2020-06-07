using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ProjetoApollo.Controllers
{
    public abstract class ProjetoApolloControllerBase: AbpController
    {
        protected ProjetoApolloControllerBase()
        {
            LocalizationSourceName = ProjetoApolloConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
