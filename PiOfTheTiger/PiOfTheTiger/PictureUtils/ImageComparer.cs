using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PiOTTDAL.Queries;
using PiOTTDAL.Constants;
using PiOTTCommon.CustomExceptions;

namespace PictureUtils
{
    public static class ImageComparer
    {
        private static decimal _totalPixels;
        private static decimal _differentPixels;

        private static decimal PercentDifferent
        {
            get
            {
                if (_differentPixels == 0M)
                    return 0M;

                return (100M * _differentPixels) / _totalPixels;
            }
        }

        private static decimal PixelCountTolerance
        {
            get
            {
                return Convert.ToDecimal(new AppSettingsQuery().GetAppSettingByKey(QueryConstants.AppSettingsKey_PicturesCompareTolerance)); //TODO: get from db
            }
        }

        private static bool IsToleranceExceeded
        {
            get
            {
                return PercentDifferent >= PixelCountTolerance;
            }
        }

        /// <summary>
        /// Compares two images and returns true if they are the same.
        /// </summary>
        /// <param name="pathImage1">Path to first image.</param>
        /// <param name="pathImage2">Path to second image.</param>
        /// <param name="tolerance">Sensitivity.</param>
        /// <returns>True if the images are the same.</returns>
        public static Boolean Compare(string pathImage1, string pathImage2)
        {
            Bitmap image1 = new Bitmap(pathImage1);
            Bitmap image2 = new Bitmap(pathImage2);

            return Compare(image1, image2);
        }

        /// <summary>
        /// Compares two images and returns true if they are the same.
        /// </summary>
        /// <param name="pathImage1">First image.</param>
        /// <param name="pathImage2">Second image.</param>
        /// <param name="tolerance">Sensitivity.</param>
        /// <returns>True if the images are the same.</returns>
        public static Boolean Compare(Bitmap image1, Bitmap image2)
        {
            // if the images are not of the same size then throw exception
            if (image1.Height != image2.Height || image1.Width != image2.Width)
                throw new ImageSizeMismatchException();

            Color image1PixelColor = Color.Transparent;
            Color image2PixelColor = Color.Transparent;

            _totalPixels = image1.Width * image1.Height;

            _differentPixels = 0;

            for (int xPixel = 0; xPixel < image1.Width; xPixel += 3)
            {
                for (int yPixel = 0; yPixel < image1.Height; yPixel += 2)
                {
                    image1PixelColor = image1.GetPixel(xPixel, yPixel);
                    image2PixelColor = image2.GetPixel(xPixel, yPixel);

                    if (image1PixelColor != image2PixelColor)
                    {
                        _differentPixels++;
                    }
                }

                if (IsToleranceExceeded)
                    return false;
            }

            return true;
        }
    }
}
