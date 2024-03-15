using IWebcamProtect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.Managers.DetectionEvents
{
    public interface IDetectionEventManager
    {
        List<DetectionEvent> GetAllByCamera(int cameraId);
    }
}
