using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PictureUtils;
using System.Drawing;
using System.IO;

namespace PioTTTests.PictureUtils
{
    [TestClass]
    public class ImageComparerTest
    {
        [TestMethod]
        public void CompareSameImages()
        {
            string pathImage1 = @".\PictureUtils\Pictures\picture001.jpg";
            string pathImage2 = @".\PictureUtils\Pictures\picture001.jpg";

            bool actual = ImageComparer.Compare(Path.GetFullPath(pathImage1), Path.GetFullPath(pathImage2));

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void CompareDifferentImagesMoreDifferences()
        {
            string pathImage1 = @".\PictureUtils\Pictures\picture001.jpg";
            string pathImage2 = @".\PictureUtils\Pictures\picture002.jpg";

            bool actual = ImageComparer.Compare(Path.GetFullPath(pathImage1), Path.GetFullPath(pathImage2));

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void CompareDifferentImagesLessDifferences()
        {
            string pathImage1 = @".\PictureUtils\Pictures\picture002.jpg";
            string pathImage2 = @".\PictureUtils\Pictures\picture003.jpg";

            bool actual = ImageComparer.Compare(pathImage1, pathImage2);

            Assert.AreEqual(true, actual);
        }
    }
}
