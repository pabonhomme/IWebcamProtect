using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using IWebcamProtect.DetectionEvents.Dto;
using IWebcamProtect.Models;
using System;
using System.Collections.Generic;

namespace IWebcamProtect.Cameras.Dto
{
    [AutoMap(typeof(Camera))]
    public class CameraDto : FullAuditedEntityDto<int>
    {
        public string Reference { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public List<DetectionEventDto> DetectionEvents { get; set; }
    }
}
