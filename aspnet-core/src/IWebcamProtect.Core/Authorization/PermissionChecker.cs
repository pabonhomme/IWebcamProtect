using Abp.Authorization;
using IWebcamProtect.Authorization.Roles;
using IWebcamProtect.Authorization.Users;

namespace IWebcamProtect.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
