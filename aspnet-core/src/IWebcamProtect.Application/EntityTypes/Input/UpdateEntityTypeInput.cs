using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using IWebcamProtect.Models;

namespace IWebcamProtect.EntityTypes.Input
{
    [AutoMap(typeof(EntityType))]

    public class UpdateEntityTypeInput : EntityDto
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public int EmergencyLevel { get; set; }
        public bool IsActive { get; set; }
    }
}