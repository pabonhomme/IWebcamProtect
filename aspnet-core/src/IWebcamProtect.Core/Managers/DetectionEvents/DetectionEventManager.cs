using Abp.Domain.Repositories;
using IWebcamProtect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.Managers.DetectionEvents
{
    public class DetectionEventManager : IDetectionEventManager
    {
        private readonly IRepository<DetectionEvent, int> _detectionEventRepository;

        public DetectionEventManager(IRepository<DetectionEvent, int> detectionEventRepository) { 
            _detectionEventRepository= detectionEventRepository;
        }

        public List<DetectionEvent> GetAllByCamera(int cameraId)
        {
            var list = _detectionEventRepository.GetAllIncluding(e => e.EntityType).Where(entity => entity.CameraId == cameraId).ToList();

            return list;
        }
    }
}
