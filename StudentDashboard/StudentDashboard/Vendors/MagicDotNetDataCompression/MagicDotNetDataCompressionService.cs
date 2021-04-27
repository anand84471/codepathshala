using ImageMagick;
using StudentDashboard.Models.Files;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StudentDashboard.Vendors.MagicDotNetDataCompression
{
    public class MagicDotNetDataCompressionService
    {
        //https://github.com/dlemstra/Magick.NET/blob/master/docs/Readme.md 
        //Documentation
        public void CompressImage(String filePath)
        {
            var snakewareLogo = new FileInfo(filePath);
            var optimizer = new ImageOptimizer();
            optimizer.LosslessCompress(snakewareLogo);
            snakewareLogo.Refresh();
        }
        public FileModal ResizeImageWithoutMaintainingAspectRatio(String filePath,int width, int height)
        {
            FileModal fileModal = new FileModal();
            filePath = HttpContext.Current.Server.MapPath(filePath);
            using (var image = new MagickImage(filePath))
            {
                var size = new MagickGeometry(width, height);
                // This will resize the image to a fixed size without maintaining the aspect ratio.
                // Normally an image will be resized to fit inside the specified size.
                size.IgnoreAspectRatio = true;
                image.Resize(size);
                // Save the result
                string fileDirectoryName= MasterUtilities.GetDirectoryOfFile(filePath, (int)Constants.FilePathTypeId.ABSOLUTE_PATH);
                string extensionName = MasterUtilities.GetFileExtensionName(filePath, (int)Constants.FilePathTypeId.ABSOLUTE_PATH);
                string fileName = MasterUtilities.GetFileName(filePath, (int)Constants.FilePathTypeId.ABSOLUTE_PATH);
                fileModal.m_strFilePath = fileDirectoryName +"\\" +fileName+ "_" +width + "x" + height +"_"+ extensionName;
                fileModal.m_strFileName = fileName+ "_" + width + "x" + height + "_" + extensionName;
                image.Write(fileModal.m_strFilePath);
            }
            return fileModal;
        }
        public string ResizeImageWitMaintainingAspectRatio(String filePath,int Width)
        {
            string newFilePath = null;
            // Read from file
            using (var image = new MagickImage(filePath))
            {
                // Resize each image in the collection to a width of 200. When zero is specified for the height
                // the height will be calculated with the aspect ratio.
                string fileDirectoryName = MasterUtilities.GetDirectoryOfFile(filePath, (int)Constants.FilePathTypeId.ABSOLUTE_PATH);
                string extensionName = MasterUtilities.GetFileExtensionName(filePath, (int)Constants.FilePathTypeId.ABSOLUTE_PATH);
                image.Resize(Width, 0);
                // Save the result
                newFilePath = fileDirectoryName + "_" + Width + "_" + extensionName;
                image.Write(newFilePath);
            }
            return newFilePath;
        }
    }
}