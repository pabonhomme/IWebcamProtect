using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using IWebcamProtect.Authorization;
using IWebcamProtect.Cameras.Dto;
using IWebcamProtect.Cameras.Input;
using IWebcamProtect.DetectionEvents.Dto;
using IWebcamProtect.DetectionEvents.Input;
using IWebcamProtect.Managers.Cameras;
using IWebcamProtect.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.DetectionEvents
{
    public class DetectionEventAppService : AsyncCrudAppService<DetectionEvent, DetectionEventDto, int, GetAllDetectionEventInput, CreateDetectionEventInput, UpdateDetectionEventInput, GetDetectionEventInput, DeleteDetectionEventInput>, IDetectionEventAppService
    {
        private ICameraManager _cameraManager;
        public DetectionEventAppService(IRepository<DetectionEvent, int> repository, ICameraManager cameraManager) : base(repository)
        {
            LocalizationSourceName = IWebcamProtectConsts.LocalizationSourceName;
            GetPermissionName = PermissionNames.Pages_DetectionEvent_Read;
            DeletePermissionName = PermissionNames.Pages_DetectionEvent_Delete;
            //CreatePermissionName = PermissionNames.Pages_DetectionEvent_Edit;
            UpdatePermissionName = PermissionNames.Pages_DetectionEvent_Edit;

            _cameraManager= cameraManager;
        }

        #region Create

        /// <summary>
        /// Create a detection event
        /// </summary>
        /// <param name="input">detection event infos</param>
        /// <returns>DetectionEventDto if the creation went well</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public override async Task<DetectionEventDto> CreateAsync([FromBody] CreateDetectionEventInput input)
        {
            if (input == null) throw new ArgumentNullException();

            var camera = _cameraManager.GetCameraByReference(input.CameraReference);

            if (camera == null) throw new UserFriendlyException("La caméra n'existe pas");

            var detectionEventDto = await base.CreateAsync(input); // creation of the camera

            return detectionEventDto;
        }

        #endregion


    }
}
