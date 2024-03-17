using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using IWebcamProtect.Authorization;
using IWebcamProtect.EntityTypes.Dto;
using IWebcamProtect.EntityTypes.Input;
using IWebcamProtect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.EntityTypes
{
    [AbpAuthorize(PermissionNames.Pages_EntityType)]
    public class EntityTypeAppService : AsyncCrudAppService<EntityType, EntityTypeDto, int, GetAllEntityTypeInput, CreateEntityTypeInput, UpdateEntityTypeInput, GetEntityTypeInput, DeleteEntityTypeInput>, IEntityTypeAppService
    {
        public EntityTypeAppService(IRepository<EntityType, int> repository) : base(repository)
        {
            LocalizationSourceName = IWebcamProtectConsts.LocalizationSourceName;
            GetPermissionName = PermissionNames.Pages_EntityType_Read;
            DeletePermissionName = PermissionNames.Pages_EntityType_Delete;
            CreatePermissionName = PermissionNames.Pages_EntityType_Edit;
            UpdatePermissionName = PermissionNames.Pages_EntityType_Edit;
        }

        /// <summary>
        /// Get all entity types
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_EntityType_Read)]
        public override async Task<PagedResultDto<EntityTypeDto>> GetAllAsync(GetAllEntityTypeInput input)
        {
            return await base.GetAllAsync(input);
        }

        /// <summary>
        /// Get all active entity types
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<EntityTypeDto>> GetActivesAsync(GetAllEntityTypeInput input)
        {
            CheckGetAllPermission();

            var query = Repository.GetAll().Where(r => r.IsActive);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<EntityTypeDto>(
                           totalCount,
                           entities.Select(MapToEntityDto).ToList()
                       );
        }


    }
}
