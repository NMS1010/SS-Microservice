using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace SS_Microservice.Common.Services.Upload
{
    public class UploadService : IUploadService
    {
        private readonly Cloudinary _cloudinary;

        public UploadService()
        {
            CloudinaryOptions cloudinaryOptions = new();
            var account = new Account(cloudinaryOptions.CloudName, cloudinaryOptions.APIKey, cloudinaryOptions.APISecret);
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        private static string GetPublicId(string url)
        {
            return Path.GetFileNameWithoutExtension(url);
        }

        public void DeleteFile(string url)
        {
            var publicId = GetPublicId(url);
            _ = Task.Run(() => _cloudinary.Destroy(new(publicId)));
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            if (file == null) return "";

            var uploadParams = new RawUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.Url.AbsoluteUri;
        }
    }
}