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
        public string Reference { get; set; }

        public string Name { get; set; }

        public int State { get; set; }

    }
}
