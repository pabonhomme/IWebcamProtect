using Abp.Application.Services;
using IWebcamProtect.EntityTypes.Dto;
using IWebcamProtect.EntityTypes.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.EntityTypes
{
    public interface IEntityTypeAppService : IAsyncCrudAppService<EntityTypeDto, int , GetAllEntityTypeInput, CreateEntityTypeInput, UpdateEntityTypeInput, GetEntityTypeInput, DeleteEntityTypeInput>
    {
    }
}
