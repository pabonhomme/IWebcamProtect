using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace IWebcamProtect.Controllers
{
    public abstract class IWebcamProtectControllerBase: AbpController
    {
        protected IWebcamProtectControllerBase()
        {
            LocalizationSourceName = IWebcamProtectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
