using Abp.AutoMapper;
using IWebcamProtect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWebcamProtect.Cameras.Input
{
    [AutoMap(typeof(Camera))]
    public class CreateCameraInput
    {
        public string Name { get; set; }
        public DateTime? WatchTimeStart { get; set; }
        public int WatchDuration { get; set; }
        public int State { get; set; }

        public long? UserId { get; set; }
    }
}
