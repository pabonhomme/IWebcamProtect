using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using IWebcamProtect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.Cameras.Input
{
    [AutoMap(typeof(Camera))]
    public class UpdateCameraByCameraInput : EntityDto
    {
        public DateTime? WatchTimeStart { get; set; }
        public int WatchDuration { get; set; }
    }
}
