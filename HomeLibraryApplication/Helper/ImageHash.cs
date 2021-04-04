using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibraryApplication.Helper
{
    public class ImageHash
    {
        public string GetHash(string filePath)
        {
            using (var image = (Bitmap)Image.FromFile(filePath))
                return GetHash(image);
        }

        public string GetHash(Bitmap bitmap)
        {
            var formatter = new BinaryFormatter();

            using (var memoryStream = new MemoryStream())
            {
                var metafields = GetMetaFields(bitmap).ToArray();

                if (metafields.Any())
                    formatter.Serialize(memoryStream, metafields);

                var pixelBytes = GetPixelBytes(bitmap);
                memoryStream.Write(pixelBytes, 0, pixelBytes.Length);

                using (var hashAlgorithm = GetHashAlgorithm())
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var hash = hashAlgorithm.ComputeHash(memoryStream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private static HashAlgorithm GetHashAlgorithm() => SHA256.Create();

        private static byte[] GetPixelBytes(Bitmap bitmap, PixelFormat pixelFormat = PixelFormat.Format32bppRgb)
        {
            var lockedBits = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, pixelFormat);

            var bufferSize = lockedBits.Height * lockedBits.Stride;
            var buffer = new byte[bufferSize];
            Marshal.Copy(lockedBits.Scan0, buffer, 0, bufferSize);

            bitmap.UnlockBits(lockedBits);

            return buffer;
        }

        private static IEnumerable<KeyValuePair<string, string>> GetMetaFields(Image image)
        {
            string manufacturer = System.Text.Encoding.ASCII.GetString(image.PropertyItems[1].Value);

            yield return new KeyValuePair<string, string>("manufacturer", manufacturer);

            // return any other fields you may be interested in
        }
    }
}
