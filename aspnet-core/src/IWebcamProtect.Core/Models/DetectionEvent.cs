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
        DateTime? DetectedTime { get; set; }

        String? ImageBase64 { get; set; }

        [ForeignKey("EntityTypeId")]
        public EntityType EntityType { get; set; }
        public int EntityTypeId { get; set; }

    }
}
