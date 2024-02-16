using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using IWebcamProtect.EntityFrameworkCore;
using IWebcamProtect.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace IWebcamProtect.Web.Tests
{
    [DependsOn(
        typeof(IWebcamProtectWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class IWebcamProtectWebTestModule : AbpModule
    {
        public IWebcamProtectWebTestModule(IWebcamProtectEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(IWebcamProtectWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(IWebcamProtectWebMvcModule).Assembly);
        }
    }
}