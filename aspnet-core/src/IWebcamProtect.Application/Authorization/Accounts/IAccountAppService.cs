using System.Threading.Tasks;
using Abp.Application.Services;
using IWebcamProtect.Authorization.Accounts.Dto;

namespace IWebcamProtect.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
