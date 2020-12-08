using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageLibrary
{
    public class ImageProcessing
    {
        public static WriteableBitmap Result;

        public static void ToMainColors(BitmapImage InputImage)
        {
            try
            {
                int width = (int)InputImage.PixelWidth;
                int height = (int)InputImage.PixelHeight;
                WriteableBitmap bitmap = new WriteableBitmap(InputImage);
                int stride = width * ((bitmap.Format.BitsPerPixel + 7) / 8);
                int arraySize = stride * height;
                byte[] pixels = new byte[arraySize];
                bitmap.CopyPixels(pixels, stride, 0);

                Task<byte[]> t = Task<byte[]>.Run(() =>
                {
                    return PixelsToMainColors(pixels);
                });
                pixels = t.Result;
                Int32Rect rect = new Int32Rect(0, 0, width, height);
                bitmap.WritePixels(rect, pixels, stride, 0);
                Result = bitmap;
            }
            catch(Exception e) {
                MessageBox.Show("Cannot convert image:\n" + e.ToString(), "Exception");
            }
        }

        public async static void ToMainColorsAsync(BitmapImage InputImage)
        {
            try
            {
                int width = (int)InputImage.PixelWidth;
                int height = (int)InputImage.PixelHeight;
                WriteableBitmap bitmap = new WriteableBitmap(InputImage);
                int stride = width * ((bitmap.Format.BitsPerPixel + 7) / 8);
                int arraySize = stride * height;
                byte[] pixels = new byte[arraySize];
                bitmap.CopyPixels(pixels, stride, 0);

                Task<byte[]> t = Task<byte[]>.Run(() =>
                {
                    for (int i = 0; i < ((pixels.Length) / 4); i++)
                    {
                        int index = i * 4;
                        byte red = pixels[index];
                        byte green = pixels[index + 1];
                        byte blue = pixels[index + 2];
                        byte alpha = pixels[index + 3];

                        if (red > green && red > blue)
                        {
                            pixels[index] = 255;
                            pixels[index + 1] = 0;
                            pixels[index + 2] = 0;
                        }
                        else if (green > red && green > blue)
                        {
                            pixels[index] = 0;
                            pixels[index + 1] = 255;
                            pixels[index + 2] = 0;
                        }
                        else
                        {
                            pixels[index] = 0;
                            pixels[index + 1] = 0;
                            pixels[index + 2] = 255;
                        }
                    }
                    return pixels;
                });

                await Task.WhenAll(t);
                pixels = t.Result;
                Int32Rect rect = new Int32Rect(0, 0, width, height);
                bitmap.WritePixels(rect, pixels, stride, 0);
                Result = bitmap;
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot convert image:\n" + e.ToString(), "Exception");
            }
        }

        public static BitmapImage ByteArrayToBitmapImage(byte[] array)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(array))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        public static byte[] PixelsToMainColors(byte[] pixels)
        {
            for (int i = 0; i < ((pixels.Length) / 4); i++)
            {
                int index = i * 4;
                byte red = pixels[index];
                byte green = pixels[index + 1];
                byte blue = pixels[index + 2];
                byte alpha = pixels[index + 3];

                if (red > green && red > blue)
                {
                    pixels[index] = 255;
                    pixels[index + 1] = 0;
                    pixels[index + 2] = 0;
                }
                else if (green > red && green > blue)
                {
                    pixels[index] = 0;
                    pixels[index + 1] = 255;
                    pixels[index + 2] = 0;
                }
                else
                {
                    pixels[index] = 0;
                    pixels[index + 1] = 0;
                    pixels[index + 2] = 255;
                }
            }

            return pixels;
        }
    }
}
