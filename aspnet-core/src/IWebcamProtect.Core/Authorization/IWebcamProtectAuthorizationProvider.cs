using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace IWebcamProtect.Authorization
{
    public class IWebcamProtectAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            // IWEBCAMPROTECT

            var entityTypes_perm = context.CreatePermission(PermissionNames.Pages_EntityType, L("EntityTypes"));
            entityTypes_perm.CreateChildPermission(PermissionNames.Pages_EntityType_Read, L("ReadEntityTypes"));
            entityTypes_perm.CreateChildPermission(PermissionNames.Pages_EntityType_Edit, L("EditEntityTypes"));
            entityTypes_perm.CreateChildPermission(PermissionNames.Pages_EntityType_Delete, L("DeleteEntityTypes"));

            var camera_perm = context.CreatePermission(PermissionNames.Pages_Camera, L("Cameras"));
            camera_perm.CreateChildPermission(PermissionNames.Pages_Camera_Read, L("ReadCameras"));
            camera_perm.CreateChildPermission(PermissionNames.Pages_Camera_Edit, L("EditCameras"));
            camera_perm.CreateChildPermission(PermissionNames.Pages_Camera_Delete, L("DeleteCameras"));

            var detectionEvent_perm = context.CreatePermission(PermissionNames.Pages_DetectionEvent, L("DetectionEvents"));
            detectionEvent_perm.CreateChildPermission(PermissionNames.Pages_DetectionEvent_Read, L("ReadDetectionEvents"));
            detectionEvent_perm.CreateChildPermission(PermissionNames.Pages_DetectionEvent_Edit, L("EditDetectionEvents"));
            detectionEvent_perm.CreateChildPermission(PermissionNames.Pages_DetectionEvent_Delete, L("DeleteDetectionEvents"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, IWebcamProtectConsts.LocalizationSourceName);
        }
    }
}
