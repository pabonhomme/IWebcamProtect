using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using IWebcamProtect.Configuration;
using IWebcamProtect.Authentication.External;
using System.Collections.Generic;
using IWebcamProtect.Authentication.Google;

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

        public override void PostInitialize()
        {
            var externalAuthConfiguration = IocManager.Resolve<IExternalAuthConfiguration>();

            externalAuthConfiguration.Providers.AddRange(new List<ExternalLoginProviderInfo>() {
                new ExternalLoginProviderInfo(
                    "ExternalAuth",
                    _appConfiguration["Authentication:Google:ClientId"],
                    _appConfiguration["Authentication:Google:ClientSecret"],
                    typeof(GoogleAuthProviderApi)
                )
            });
        }
    }
}
