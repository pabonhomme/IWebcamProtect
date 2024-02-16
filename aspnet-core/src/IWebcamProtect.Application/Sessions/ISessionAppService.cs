using System.Threading.Tasks;
using Abp.Application.Services;
using IWebcamProtect.Sessions.Dto;

namespace IWebcamProtect.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
