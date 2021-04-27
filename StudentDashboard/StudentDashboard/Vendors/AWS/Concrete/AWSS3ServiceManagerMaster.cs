using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using StudentDashboard.Utilities;
using StudentDashboard.Vendors.AWS.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.Vendors.AWS
{
    public class AWSS3ServiceManagerMaster
    {
        private const string keyName = "*** provide a name for the uploaded object ***";
        private const string filePath = "*** provide the full path name of the file to upload ***";
        // Specify your bucket region (an example region is shown).
        private   RegionEndpoint bucketRegion;
        private static IAmazonS3 s3Client;
        private  AWSS3ServiceEntity _aWSS3ServiceEntity;
        private TransferUtility fileTransferUtility;
        public AWSS3ServiceManagerMaster()
        {
            _aWSS3ServiceEntity = new AWSS3ServiceEntity();
            bucketRegion = RegionEndpoint.APSoutheast1;
            s3Client = new AmazonS3Client(this.bucketRegion);
            fileTransferUtility = new TransferUtility(s3Client);
        }
        private string getAwsUrl(string FileName)
        {
            return MvcApplication._strAwsFileUploadBaseUrl + FileName;
        }
        public async Task<string> TaskImageFileAsync(string filePath, string FileName)
        {
            string awsFilePath = null;
            try
            {
               
                filePath = MasterUtilities.GetPhysicalPath(filePath);
                if(filePath!=null)
                {
                    await fileTransferUtility.UploadAsync(filePath, this._aWSS3ServiceEntity.GetBucketName() + "/" +
                   MvcApplication._strAwsBucketFolderInstructor + "/Images/Concrete");
                    awsFilePath = getAwsUrl(
                        MvcApplication._strAwsBucketFolderInstructor + "/Images/Concrete/" + FileName);
                }
            }
            catch(Exception Ex)
            {

            }
            return awsFilePath;
        }
        public async Task<string> TaskVideoFileAsync(string filePath, string FileName)
        {
            string awsFilePath = null;
            try
            {
                filePath = HttpContext.Current.Server.MapPath(filePath);
                await fileTransferUtility.UploadAsync(filePath, this._aWSS3ServiceEntity.GetBucketName() + "/" +
                    MvcApplication._strAwsBucketFolderInstructor + "/Videos/Course");
                awsFilePath = getAwsUrl(
                    MvcApplication._strAwsBucketFolderInstructor + "/Videos/Course/" + FileName);
            }
            catch (Exception Ex)
            {

            }
            return awsFilePath;
        }
        public async Task<string> TaskPdfFileAsync(string filePath, string FileName)
        {
            string awsFilePath = null;
            try
            {
                filePath = HttpContext.Current.Server.MapPath(filePath);
                await fileTransferUtility.UploadAsync(filePath, this._aWSS3ServiceEntity.GetBucketName() + "/" +
                    MvcApplication._strAwsBucketFolderInstructor + "/Pdfs");
                awsFilePath = getAwsUrl(
                    MvcApplication._strAwsBucketFolderInstructor + "/Pdfs/" + FileName);
            }
            catch (Exception Ex)
            {

            }
            return awsFilePath;
        }
        public async Task<string> TaskCustomFileAsync(string filePath, string FileName)
        {
            string awsFilePath = null;
            try
            {
                filePath = HttpContext.Current.Server.MapPath(filePath);
                await fileTransferUtility.UploadAsync(filePath, this._aWSS3ServiceEntity.GetBucketName() + "/" +
                    MvcApplication._strAwsBucketFolderInstructor + "/Custom");
                awsFilePath = getAwsUrl(
                    MvcApplication._strAwsBucketFolderInstructor + "/Custom/" + FileName);
            }
            catch (Exception Ex)
            {

            }
            return awsFilePath;
        }


        public async Task UploadFileAsync(string filePath,int requestType,string FileName)
        {
            try
            {
                
                filePath = HttpContext.Current.Server.MapPath(filePath);
                await fileTransferUtility.UploadAsync(filePath, this._aWSS3ServiceEntity.GetBucketName());
                //Console.WriteLine("Upload 1 completed");
                //// Option 3. Upload data from a type of System.IO.Stream.
                //using (var fileToUpload =
                //    new FileStream(filePath, FileMode.Open, FileAccess.Read))
                //{
                //    await fileTransferUtility.UploadAsync(fileToUpload,
                //                               _aWSS3ServiceEntity.GetBucketName(), FileName);
                //}
                Console.WriteLine("Upload 3 completed");
                // Option 4. Specify advanced settings.
                //var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                //{
                //    BucketName = _aWSS3ServiceEntity.GetBucketName(),
                //    FilePath = _aWSS3ServiceEntity.get,
                //    StorageClass = S3StorageClass.StandardInfrequentAccess,
                //    PartSize = 6291456, // 6 MB.
                //    Key = keyName,
                //    CannedACL = S3CannedACL.PublicRead
                //};
                //fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
                //fileTransferUtilityRequest.Metadata.Add("param2", "Value2");

                //await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
                //Console.WriteLine("Upload 4 completed");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }
        public async Task<string> TaskCompressedImageFileAsync(string filePath, string FileName)
        {
            string awsFilePath = null;
            try
            {
                filePath = MasterUtilities.GetPhysicalPath(filePath);
                await fileTransferUtility.UploadAsync(filePath, this._aWSS3ServiceEntity.GetBucketName() + "/" +
                    MvcApplication._strAwsBucketFolderInstructor + "/Images/Compressed");
                awsFilePath = getAwsUrl(
                    MvcApplication._strAwsBucketFolderInstructor + "/Images/Compressed/" + FileName);
            }
            catch (Exception Ex)
            {

            }
            return awsFilePath;
        }
        public async Task<string> TaskResizedImageFileAsync(string filePath, string FileName)
        {
            string awsFilePath = null;
            try
            {
                filePath = HttpContext.Current.Server.MapPath(filePath);
                await fileTransferUtility.UploadAsync(filePath, this._aWSS3ServiceEntity.GetBucketName() + "/" +
                    MvcApplication._strAwsBucketFolderInstructor + "/Images/Resized");
                awsFilePath = getAwsUrl(
                    MvcApplication._strAwsBucketFolderInstructor + "/Images/Resized/" + FileName);
            }
            catch (Exception Ex)
            {

            }
            return awsFilePath;
        }
    }
}
