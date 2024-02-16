using Abp.Application.Services;
using IWebcamProtect.MultiTenancy.Dto;

namespace IWebcamProtect.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

