using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiOTTCommon.CustomExceptions
{
    public class ImageSizeMismatchException : Exception
    {
        public ImageSizeMismatchException()
            : base("Image size mismatch.")
        {
        }

        public ImageSizeMismatchException(string message)
            : base(message)
        {
        }

        public ImageSizeMismatchException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
