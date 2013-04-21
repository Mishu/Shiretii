using PiOTTWebCam.ContinuousCapturing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PiOfTheTiger
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ContinuousCapture capturing = new ContinuousCapture();
                capturing.StartCapturing();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            Thread th = new Thread(KeepRunning);
            th.Start();
        }

        private static void KeepRunning()
        {
            while (true)
            {
                Thread.Sleep(60000);
            }
        }
    }
}
