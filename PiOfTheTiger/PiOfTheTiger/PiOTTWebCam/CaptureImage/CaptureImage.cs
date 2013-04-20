using PiOTTDAL.Entities;
using RaspberryCam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTWebCam.CaptureImage
{
    public class CaptureImage
    {
        public void TakePicture(Camera camera)
        {
            var cameras = Cameras.DeclareDevice().Named(camera.CameraName).WithDevicePath(camera.Path).AndDevice().Memorize();
            
            cameras.Get(camera.CameraName).SavePicture(new PictureSize(640, 480), "Test1.jpg", 20);
        }
    }
}
