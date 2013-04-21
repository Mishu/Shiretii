﻿using EmailUtils;
using PictureUtils;
using PiOTTDAL.Constants;
using PiOTTDAL.Entities;
using PiOTTDAL.Queries;
using PiOTTWebCam.CaptureImages;
using RaspberryCam;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PiOTTWebCam.ContinuousCapturing
{
    public class ContinuousCapture
    {
        private Cameras cameras;
        private CamManagerBuilder camBuilder;
        private List<Camera> availableCameras;
        private Timer timer;
        private string picturesFolder;

        public ContinuousCapture()
        {
            InitializeCameras();

            InitializeTimer();

            picturesFolder = new AppSettingsQuery().GetAppSettingByKey(QueryConstants.AppSettingsKey_PicturesSavePath);
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
            timer.Stop();
            foreach(Camera cam in availableCameras)
            {
                new CaptureImage().TakePicture(cam, cameras);

                CompareTakenPictures(cam);
            }

            string interval = new AppSettingsQuery().GetAppSettingByKey(QueryConstants.AppSettingsKey_PicturesSaveInterval);
            Double actualInterval = Double.Parse(interval);

            if (actualInterval != timer.Interval)
            {
                //timer.Stop();
                timer.Interval = actualInterval;
                //timer.Start();
            }

            timer.Start();
        }

        private void CompareTakenPictures(Camera cam)
        {
            string path = Path.Combine(picturesFolder, cam.CameraName);
            List<String> files = Directory.EnumerateFiles(path).ToList();

            if (files.Count > 2)
            {
                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
            else if (files.Count == 2)
            {
                FileInfo file0 = new FileInfo(files[0]);
                FileInfo file1 = new FileInfo(files[1]);

                if (file0.CreationTime > file1.CreationTime)
                {
                    file0 = new FileInfo(files[1]);
                    file1 = new FileInfo(files[0]);
                }

                Boolean compareResult = ImageComparer.Compare(file0.FullName, file1.FullName);

                if (!compareResult)
                {
                    new EmailCreator().SendMailForDifferentImages(file1.FullName);
                }

                File.Delete(file0.FullName);
            }
        }

        private void InitializeCameras()
        {
            availableCameras = new CameraQuery().GetAllCamera();
            camBuilder = Cameras.DeclareDevice().Named(availableCameras[1].CameraName).WithDevicePath(availableCameras[1].Path)
                .AndDevice().Named(availableCameras[0].CameraName).WithDevicePath(availableCameras[0].Path);
            //for (int camIndex = 1; camIndex < availableCameras.Count; camIndex++)
            //{
            //    camBuilder.AndDevice().Named(availableCameras[camIndex].CameraName).WithDevicePath(availableCameras[camIndex].Path);
            //}

            cameras = camBuilder.Memorize();
        }

        public void StartCapturing()
        {
            timer.Start();
        }
    }
}
