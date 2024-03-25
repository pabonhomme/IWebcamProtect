using Abp.AutoMapper;
using IWebcamProtect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.DetectionEvents.Input
{
    [AutoMap(typeof(DetectionEvent))]
    public class CreateDetectionEventInput
    {
        public DateTime DetectedTime { get; set; }

        public string ImageBase64 { get; set; }

        public int EntityTypeId { get; set; }

        public int CameraId { get; set; }

        public string CameraReference { get; set; }

    }
}
