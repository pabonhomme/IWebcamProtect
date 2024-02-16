using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using IWebcamProtect.Authorization;

namespace IWebcamProtect
{
    [DependsOn(
        typeof(IWebcamProtectCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class IWebcamProtectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<IWebcamProtectAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(IWebcamProtectApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
