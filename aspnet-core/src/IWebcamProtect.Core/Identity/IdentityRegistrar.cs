using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using IWebcamProtect.Authorization;
using IWebcamProtect.Authorization.Roles;
using IWebcamProtect.Authorization.Users;
using IWebcamProtect.Editions;
using IWebcamProtect.MultiTenancy;
using IWebcamProtect.Managers.DetectionEvents;
using IWebcamProtect.Managers.Cameras;

namespace IWebcamProtect.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            services.AddScoped<IDetectionEventManager, DetectionEventManager>();
            services.AddScoped<ICameraManager, CameraManager>();



            return services.AddAbpIdentity<Tenant, User, Role>()
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpLogInManager<LogInManager>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}
