using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using IWebcamProtect.Models;
using System;

namespace IWebcamProtect.Cameras.Dto
{
    [AutoMap(typeof(Camera))]
    public class CameraDto : FullAuditedEntityDto<int>
    {
        public string Name { get; set; }
        public DateTime? WatchTimeStart { get; set; }
        public int WatchDuration { get; set; }
        public int State { get; set; }
    }
}
