using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using IWebcamProtect.EntityTypes.Dto;
using IWebcamProtect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.DetectionEvents.Dto
{
    [AutoMap(typeof(DetectionEvent))]
    public class DetectionEventDto : EntityDto
    {
        public DateTime? DetectedTime { get; set; }

        public String? ImageBase64 { get; set; }

        public EntityTypeDto EntityType { get; set; }

    }
}
