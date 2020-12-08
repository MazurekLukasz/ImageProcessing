using System;
using ImageLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.UI.Xaml.Media.Imaging;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodForConvert()
        {
            byte[] pixels = new byte[] { 240, 200, 200, 255, 200, 240, 200, 255, 200, 200, 240, 255 };
            pixels = ImageProcessing.PixelsToMainColors(pixels);

            if (pixels[0] != 255 || pixels[5] != 255 || pixels[10] != 255)
            {
                Assert.Fail();
            }
        }
    }
}
