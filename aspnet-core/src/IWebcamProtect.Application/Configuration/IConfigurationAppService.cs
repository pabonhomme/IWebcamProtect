using System.Threading.Tasks;
using IWebcamProtect.Configuration.Dto;

namespace IWebcamProtect.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
