using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using IWebcamProtect.Models;

namespace IWebcamProtect.EntityTypes.Input
{
    [AutoMap(typeof(EntityType))]

    public class DeleteEntityTypeInput : EntityDto
    {
    }
}