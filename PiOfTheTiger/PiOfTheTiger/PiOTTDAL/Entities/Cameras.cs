using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTDAL.Entities
{
    public class Camera
    {
        public int IdCamera { get; set; }

        [Name]
        public string CameraName { get; set; }
        public string Path { get; set; }
    }
}
