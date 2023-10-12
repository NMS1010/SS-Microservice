using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

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

        public async Task DeleteFile(string url)
        {
            var publicId = GetPublicId(url);
            await _cloudinary.DestroyAsync(new(publicId));
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