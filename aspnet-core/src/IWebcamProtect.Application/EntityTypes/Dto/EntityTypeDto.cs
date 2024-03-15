using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using IWebcamProtect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.EntityTypes.Dto
{
    [AutoMap(typeof(EntityType))]
    public class EntityTypeDto : EntityDto<int>
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public int EmergencyLevel { get; set; }
        public bool IsActive { get; set; }
    }
}
