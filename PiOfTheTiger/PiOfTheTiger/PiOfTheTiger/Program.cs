using PiOTTWebCam.ContinuousCapturing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOfTheTiger
{
    class Program
    {
        static void Main(string[] args)
        {
            ContinuousCapture capturing = new ContinuousCapture();
            capturing.StartCapturing();
        }
    }
}
