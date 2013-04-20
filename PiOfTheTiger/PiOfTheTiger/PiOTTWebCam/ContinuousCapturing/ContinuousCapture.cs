using PiOTTDAL.Constants;
using PiOTTDAL.Entities;
using PiOTTDAL.Queries;
using PiOTTWebCam.CaptureImages;
using RaspberryCam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PiOTTWebCam.ContinuousCapturing
{
    public class ContinuousCapture
    {
        Cameras cameras;
        CamManager camManager;
        CamManagerBuilder camBuilder;
        List<Camera> availableCameras;
        Timer timer;

        public ContinuousCapture()
        {
            InitializeCameras();

            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new Timer();

            string interval = new AppSettingsQuery().GetAppSettingByKey(QueryConstants.AppSettingsKey_PicturesSaveInterval);

            timer.Interval = Double.Parse(interval);
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach(Camera cam in availableCameras)
            {
                new CaptureImage().TakePicture(cam, cameras);
            }

            string interval = new AppSettingsQuery().GetAppSettingByKey(QueryConstants.AppSettingsKey_PicturesSaveInterval);
            Double actualInterval = Double.Parse(interval);

            if (actualInterval != timer.Interval)
            {
                timer.Stop();
                timer.Interval = actualInterval;
                timer.Start();
            }
        }

        private void InitializeCameras()
        {
            availableCameras = new CameraQuery().GetAllCamera();
            camManager = new CamManager();
            camBuilder = new CamManagerBuilder(camManager);
            foreach (Camera camera in availableCameras)
            {
                camBuilder.AndDevice().Named(camera.CameraName).WithDevicePath(camera.Path);
            }

            cameras = camBuilder.Memorize();
        }

        public void StartCapturing()
        {
            timer.Start();
        }
    }
}
