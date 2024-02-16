using Abp.Application.Services.Dto;

namespace IWebcamProtect.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

