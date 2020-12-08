using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImageLibrary
{
    public class ImageOperations
    {
        public static BitmapImage OpenImageFromDisc()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                BitmapImage img =  new BitmapImage(fileUri);
                return img;
            }
            return null;
        }

        public static void SaveImageOnDisc(WriteableBitmap image)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg
            saveFileDialog.Filter = "BMP file | *.bmp | PNG file | *.png | JPEG file | *.jpeg";
            if (saveFileDialog.ShowDialog() == true)
            {
                FileStream saveStream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(saveStream);
                saveStream.Close();
            }
        }

        public BitmapImage ByteArrayToBitmapImage(byte[] binaryData)
        {
            BitmapImage bitmapImage = new BitmapImage();
            //
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = new MemoryStream(binaryData);
            bitmapImage.EndInit();
            //
            return bitmapImage;
        }

        public byte[] BitmapImageToByteArray(BitmapSource bitmapImage)
        {
            byte[] bytes;
            //
            using (MemoryStream ms = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(ms);
                bytes = ms.ToArray();
            }
            //
            return bytes;
        }
    }
}
