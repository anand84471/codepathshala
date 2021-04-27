using StudentDashboard.Models.Files;
using StudentDashboard.Vendors.MagicDotNetDataCompression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Utilities
{
    public class ImageCompressionUtilities
    {
        MagicDotNetDataCompressionService magicDotNetDataCompressionService;
        public ImageCompressionUtilities()
        {
            magicDotNetDataCompressionService = new MagicDotNetDataCompressionService();
        }
        public void CompressImage(String filePath)
        {
            magicDotNetDataCompressionService.CompressImage(filePath);
        }
        //Resizing to 100X100 size
        public FileModal ResizeToProfileThumbnail(string path)
        {
            FileModal fileModal = new FileModal();
            int profileThumbnailWidth = Constants.PROFILE_THUMBNAIL_SIZE;
            fileModal= magicDotNetDataCompressionService.ResizeImageWithoutMaintainingAspectRatio(path, profileThumbnailWidth, profileThumbnailWidth);
            return fileModal;
        }
        public FileModal ResizeToCourseThumbnail(string path)
        {
            int courseThumbnailWidth = Constants.COURSE_THUMBNAIL_WIDTH;
            int courseThumbnailHeight = Constants.COURSE_THUMBNAIL_HEIGHT;
            return magicDotNetDataCompressionService.ResizeImageWithoutMaintainingAspectRatio(path,courseThumbnailWidth,courseThumbnailHeight);
        }
        public FileModal ResizeToClassroomThumbnail(string path)
        {
            int classroomThumbnailWidth = Constants.CLASSROOM_THUMBNAIL_WIDTH;
            int classroomThumbnailHeight = Constants.CLASSROOM_THUMBNAIL_HEIGHT;
            return magicDotNetDataCompressionService.ResizeImageWithoutMaintainingAspectRatio(path, classroomThumbnailWidth, classroomThumbnailHeight);
        }
    }
}