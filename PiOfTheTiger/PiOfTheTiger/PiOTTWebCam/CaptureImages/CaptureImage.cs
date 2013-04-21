using PiOTTDAL.Constants;
using PiOTTDAL.Entities;
using PiOTTDAL.Queries;
using RaspberryCam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTWebCam.CaptureImages
{
    public class CaptureImage
    {
        /// <summary>
        /// Takes the picture from the given camera
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="cameras"></param>
        public void TakePicture(Camera camera, Cameras cameras)
        {
            int jpegCompressionRate = 20;
            string saveToPath = GetImageSavePath(camera);
            cameras.Get(camera.CameraName)
                .SavePicture(new PictureSize(camera.PictureWidth
                            , camera.PictureHeight)
                            , saveToPath
                            , jpegCompressionRate);
        }

        /// <summary>
        /// Gets the path and file name where to save the picture
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        private string GetImageSavePath(Camera camera)
        {
            DateTime currentDateTime = DateTime.Now;
            string folder = new AppSettingsQuery().GetAppSettingByKey(QueryConstants.AppSettingsKey_PicturesSavePath);
            folder = String.Format(@"{0}/{1}/", folder, camera.CameraName);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = String.Format("{0}[{1}-{2}-{3}][{4}-{5}-{6}].jpg"
                , camera.CameraName
                , currentDateTime.Year, currentDateTime.Month, currentDateTime.Day
                , currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);

            string saveToPath = Path.Combine(folder, fileName);
            return saveToPath;
        }
    }
}
