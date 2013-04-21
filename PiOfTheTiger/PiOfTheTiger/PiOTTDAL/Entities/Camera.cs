using PiOTTDAL.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Entities
{
    public class Camera
    {
        [ID]
        public int IdCamera { get; set; }

        [Name]
        public string CameraName { get; set; }
        public string Path { get; set; }
        public int PictureHeight { get; set; }
        public int PictureWidth { get; set; }
    }
}
