using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
                return 10M; //TODO: get from db
            }
        }

        private static decimal ColorTolerance
        {
            get
            {
                return PixelCountTolerance / 10M;
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
        public static Nullable<Bitmap> Compare(string pathImage1, string pathImage2)
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
        public static Nullable<Bitmap> Compare(Bitmap image1, Bitmap image2)
        {
            // if the images are not of the same size then throw exception
            if (image1.Height != image2.Height || image1.Width != image2.Width)
                throw new Exception("Mismatch image size"); //TODO: replace with custom exception

            Bitmap resultImage = new Bitmap(image1.Width, image1.Height);

            // declare and init the pixels with transparent
            Color image1PixelColor = Color.Transparent;
            Color image2PixelColor = Color.Transparent;

            // get total pixels of the first image
            _totalPixels = image1.Width * image1.Height;

            // reinit the number of different pixels between the images
            _differentPixels = 0;

            for (int xPixel = 0; xPixel < image1.Width; xPixel++)
            {
                for (int yPixel = 0; yPixel < image1.Height; yPixel++)
                {
                    image1PixelColor = image1.GetPixel(xPixel, yPixel);
                    image2PixelColor = image2.GetPixel(xPixel, yPixel);

                    if (ArePixelsDifferent(image1PixelColor, image2PixelColor))
                    {
                        _differentPixels++;
                        resultImage.SetPixel(xPixel, yPixel, Color.Red);
                    }
                }
            }

            if (IsToleranceExceeded)
                return resultImage;
            else
                return null;
        }

        private static bool ArePixelsDifferent(Color image1PixelColor, Color image2PixelColor)
        {
            return AreColorsDifferent(image1PixelColor.R, image2PixelColor.R)
                    || AreColorsDifferent(image1PixelColor.G, image2PixelColor.G)
                    || AreColorsDifferent(image1PixelColor.B, image2PixelColor.B);
        }

        private static bool AreColorsDifferent(byte pixelColor1, byte pixelColor2)
        {
            decimal diff = Math.Abs(pixelColor1 - pixelColor2);

            if (pixelColor1 * pixelColor2 == 0)
            {
                return diff < (ColorTolerance);
            }
            else
            {
                return diff / (pixelColor1 + pixelColor2) > ColorTolerance;
            }
        }


    }
}
