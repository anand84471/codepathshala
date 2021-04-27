using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace StudentDashboard.Utilities
{
    public class ImageCompressionUtil
    {
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        public static void CompressImage(string SoucePath, string DestPath, int quality)
        {
            var FileName = Path.GetFileName(SoucePath);
            DestPath = DestPath + "\\" + FileName;

            using (Bitmap bmp1 = new Bitmap(SoucePath))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                System.Drawing.Imaging.Encoder QualityEncoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(QualityEncoder, quality);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(DestPath, jpgEncoder, myEncoderParameters);
            }
        }
    }
}