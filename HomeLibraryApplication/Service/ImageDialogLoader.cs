using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace HomeLibraryApplication.Service
{
    public static class ImageDialogLoader
    {
        private static BitmapImage BitmapImage()
        {
            OpenFileDialog openFileDialog_LoadImage = new OpenFileDialog();
            openFileDialog_LoadImage.Filter = "Image files(*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            var file = openFileDialog_LoadImage.ShowDialog();
            BitmapImage filefoto = null;
            if (file == true)
            {
                string filename = openFileDialog_LoadImage.FileName;
                filefoto = new BitmapImage(new Uri(filename));
            }

            return filefoto;
        }

        public static byte[] ImageToByte(BitmapImage imageSource)
        {
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageSource));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
    }
}
