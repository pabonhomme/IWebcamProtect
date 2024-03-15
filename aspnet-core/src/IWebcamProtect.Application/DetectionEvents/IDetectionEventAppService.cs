using Abp.Application.Services;
using IWebcamProtect.DetectionEvents.Dto;
using IWebcamProtect.DetectionEvents.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.DetectionEvents
{
    public interface IDetectionEventAppService : IAsyncCrudAppService<DetectionEventDto, int, GetAllDetectionEventInput, CreateDetectionEventInput, UpdateDetectionEventInput, GetDetectionEventInput, DeleteDetectionEventInput>
    {
    }
}
