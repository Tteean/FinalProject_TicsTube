using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Implementations
{
    public class VideoService
    {
        private readonly Cloudinary _cloudinary;

        public VideoService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            using var stream = file.OpenReadStream();
            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "Videos"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult?.SecureUrl?.AbsoluteUri;
        }
        public async Task<bool> DeleteVideoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

            return deleteResult.Result == "ok" || deleteResult.Result == "not found";
        }
        

        public async Task<string> EditVideoAsync(string publicId, IFormFile newFile)
        {
            await DeleteVideoAsync(publicId);
            return await UploadVideoAsync(newFile);
        }
    }

}
