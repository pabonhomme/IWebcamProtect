using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using IWebcamProtect.Configuration;

namespace IWebcamProtect.Web.Host.Startup
{
    [DependsOn(
       typeof(IWebcamProtectWebCoreModule))]
    public class IWebcamProtectWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public IWebcamProtectWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(IWebcamProtectWebHostModule).GetAssembly());
        }
    }
}
