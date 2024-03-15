using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.Models
{
    public class EntityType : Entity, IPassivable
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public int EmergencyLevel { get; set; }
        public bool IsActive { get; set; }
    }
}
