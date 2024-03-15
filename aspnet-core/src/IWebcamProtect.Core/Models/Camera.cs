using Abp.Domain.Entities.Auditing;
using IWebcamProtect.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.Models
{
    public class Camera : FullAuditedEntity<int, User>
    {
        public string Name { get; set; }
        public DateTime? WatchTimeStart { get; set; }
        public int WatchDuration { get; set; }
        public int State { get; set; }

        public List<DetectionEvent> DetectionEvents { get; set; } = new List<DetectionEvent>();
    }
}
