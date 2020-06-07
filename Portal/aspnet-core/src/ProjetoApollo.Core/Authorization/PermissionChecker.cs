using Abp.Authorization;
using ProjetoApollo.Authorization.Roles;
using ProjetoApollo.Authorization.Users;

namespace ProjetoApollo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
