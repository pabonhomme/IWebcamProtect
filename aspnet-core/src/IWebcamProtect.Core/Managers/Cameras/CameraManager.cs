using Abp.Domain.Repositories;
using IWebcamProtect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.Managers.Cameras
{
    public class CameraManager : ICameraManager
    {
        private readonly IRepository<Camera, int> _cameraRepository;

        public CameraManager(IRepository<Camera, int> cameraRepository) { 
            _cameraRepository = cameraRepository;
        }

        public Camera GetCameraByReference(string cameraRef)
        {
           return _cameraRepository.GetAll().Where(c => c.Reference == cameraRef).FirstOrDefault();
        }
    }
}
