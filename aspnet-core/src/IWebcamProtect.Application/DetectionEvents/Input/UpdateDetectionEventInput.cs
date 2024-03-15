using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using IWebcamProtect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.DetectionEvents.Input
{
    [AutoMap(typeof(DetectionEvent))]
    public class UpdateDetectionEventInput : EntityDto
    {
    }
}
