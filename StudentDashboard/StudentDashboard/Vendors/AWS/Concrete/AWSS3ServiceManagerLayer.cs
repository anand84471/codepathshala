using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.Vendors.AWS.Concrete
{
    public class AWSS3ServiceManagerLayer
    {
        AWSS3ServiceManagerMaster _aWSS3ServiceManagerMaster;
        public AWSS3ServiceManagerLayer()
        {
            _aWSS3ServiceManagerMaster = new AWSS3ServiceManagerMaster();
        }
        public async Task<string> UploadImageFileAsync(string fileKeyName,string FilePath,int ClientRequsetType )
        {
            return await _aWSS3ServiceManagerMaster.TaskImageFileAsync(FilePath, fileKeyName);
        }
        public async Task<string> UploadVideoFileAsync(string fileKeyName, string FilePath, int ClientRequsetType)
        {
            return await _aWSS3ServiceManagerMaster.TaskVideoFileAsync(FilePath, fileKeyName);
        }
        public async Task<string> UploaddPdfFileAsync(string fileKeyName, string FilePath, int ClientRequsetType)
        {
            return await _aWSS3ServiceManagerMaster.TaskPdfFileAsync(FilePath, fileKeyName);
        }
        public async Task<string> UploaddCustomeFileAsync(string fileKeyName, string FilePath, int ClientRequsetType)
        {
            return await _aWSS3ServiceManagerMaster.TaskCustomFileAsync(FilePath, fileKeyName);
        }
        public async Task<string> UploadImageFileCompressedAsync(string fileKeyName, string FilePath, int ClientRequsetType)
        {
            return await _aWSS3ServiceManagerMaster.TaskCompressedImageFileAsync(FilePath, fileKeyName);
        }
        public async Task<string> UploadResizedFileCompressedAsync(string fileKeyName, string FilePath, int ClientRequsetType)
        {
            return await _aWSS3ServiceManagerMaster.TaskResizedImageFileAsync(FilePath, fileKeyName);
        }

    }
}