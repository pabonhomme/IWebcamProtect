﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using IWebcamProtect.Authorization;
using IWebcamProtect.Cameras.Dto;
using IWebcamProtect.Cameras.Input;
using IWebcamProtect.Managers.DetectionEvents;
using IWebcamProtect.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.Cameras
{
    [AbpAuthorize(PermissionNames.Pages_Camera)]
    public class CameraAppService : AsyncCrudAppService<Camera, CameraDto, int, GetAllCameraInput, CreateCameraInput, UpdateCameraInput, GetCameraInput, DeleteCameraInput>, ICameraAppService
    {
        private readonly IDetectionEventManager _detectionEventManager;
        public CameraAppService(IRepository<Camera, int> repository, IDetectionEventManager detectionEventManager) : base(repository)
        {
            LocalizationSourceName = IWebcamProtectConsts.LocalizationSourceName;
            GetPermissionName = PermissionNames.Pages_Camera_Read;
            DeletePermissionName = PermissionNames.Pages_Camera_Delete;
            CreatePermissionName = PermissionNames.Pages_Camera_Edit;
            UpdatePermissionName = PermissionNames.Pages_Camera_Edit;

            _detectionEventManager= detectionEventManager;
        }

        #region GET

        public override async Task<CameraDto> GetAsync(GetCameraInput input)
        {
            var query = Repository.GetAll().Where(e => e.Id == input.Id);

            var entity = await AsyncQueryableExecuter.ToListAsync(query);

            var detectionEvents = _detectionEventManager.GetAllByCamera(input.Id);
            detectionEvents.Sort((d1, d2) => DateTime.Compare(d2.DetectedTime, d1.DetectedTime));

            // Assigner la liste triée à l'entité
            entity.FirstOrDefault().DetectionEvents = detectionEvents;

            return MapToEntityDto(entity.FirstOrDefault());


        }

        /// <summary>
        /// Get all the connected user's cameras
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<CameraDto>> GetAllByUserAsync(GetAllCameraInput input)
        {
            CheckGetAllPermission();

            var query = Repository.GetAll().Where(e => e.CreatorUserId == AbpSession.UserId);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<CameraDto>(
                            totalCount,
                            entities.Select(MapToEntityDto).ToList()
                        );
        }

        #endregion

        #region Create

        /// <summary>
        /// Create a camera
        /// </summary>
        /// <param name="input">camera infos</param>
        /// <returns>CameraDto if the creation went well</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public override async Task<CameraDto> CreateAsync([FromForm] CreateCameraInput input)
        {
            if (input == null) throw new ArgumentNullException();

            var cameraDto = await base.CreateAsync(input); // creation of the camera

            return cameraDto;
        }

        #endregion

        #region Update

        /// <summary>
        /// Update a camera
        /// </summary>
        /// <param name="input">camera infos</param>
        /// <returns>CameraDto if the update went well</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public override async Task<CameraDto> UpdateAsync([FromForm] UpdateCameraInput input)
        {
            if (input == null) throw new ArgumentNullException();

            var query = Repository.GetAll().Where(e => e.Id == input.Id);

            var entity = await AsyncQueryableExecuter.FirstOrDefaultAsync(query);

            if(entity.CreatorUserId != AbpSession.UserId)
            {
                throw new UserFriendlyException("La caméra ne peut être modifiée que par son propriétaire");
            }

            var cameraDto = await base.UpdateAsync(input); // update of the camera

            return cameraDto;
        }

        public async Task<CameraDto> UpdateByCameraAsync([FromForm] UpdateCameraByCameraInput input)
        {
            if (input == null) throw new ArgumentNullException();

            var camera = await Repository.UpdateAsync(ObjectMapper.Map<Camera>(input)); // update of the camera

            return MapToEntityDto(camera);
        }

        #endregion
    }
}
