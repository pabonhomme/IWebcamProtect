using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using IWebcamProtect.Configuration;
using IWebcamProtect.EntityFrameworkCore;
using IWebcamProtect.Migrator.DependencyInjection;

namespace IWebcamProtect.Migrator
{
    [DependsOn(typeof(IWebcamProtectEntityFrameworkModule))]
    public class IWebcamProtectMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public IWebcamProtectMigratorModule(IWebcamProtectEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(IWebcamProtectMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                IWebcamProtectConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(IWebcamProtectMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
