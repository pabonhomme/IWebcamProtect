using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using IWebcamProtect.Configuration.Dto;

namespace IWebcamProtect.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : IWebcamProtectAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
