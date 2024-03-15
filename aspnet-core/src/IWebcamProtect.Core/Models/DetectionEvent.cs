using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.Models
{
    public class DetectionEvent : Entity
    {
        public DateTime? DetectedTime { get; set; }
        public String? ImageBase64 { get; set; }

        [ForeignKey("EntityTypeId")]
        public EntityType EntityType { get; set; }
        public int EntityTypeId { get; set; }

        [ForeignKey("CameraId")]
        public Camera Camera { get; set; }
        public int CameraId { get; set; }

    }
}
