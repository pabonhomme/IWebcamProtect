using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IWebcamProtect.Cameras.Dto;
using IWebcamProtect.Cameras.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.Cameras
{
    public interface ICameraAppService : IAsyncCrudAppService<CameraDto, int, GetAllCameraInput, CreateCameraInput, UpdateCameraInput, GetCameraInput, DeleteCameraInput>
    {
        Task<PagedResultDto<CameraDto>> GetAllByUserAsync(GetAllCameraInput input);
    }
}
