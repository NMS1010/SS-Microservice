using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Services.Upload
{
    public class UploadService : IUploadService
    {
        private readonly string _userContent;
        private const string USER_CONTENT_FOLDER = "user-content";
        private static IHttpContextAccessor _httpContextAccessor;

        public UploadService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _userContent = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER);
            _httpContextAccessor = httpContextAccessor;
            if (!Directory.Exists(_userContent))
            {
                Directory.CreateDirectory(_userContent);
            }
        }

        public async Task DeleteFile(string filename)
        {
            string path = Path.Combine(_userContent, Path.GetFileName(filename));
            if (File.Exists(path))
            {
                await Task.Run(() => File.Delete(path));
            }
        }

        private async Task<string> ConfirmSave(Stream stream, string fileName)
        {
            string filePath = Path.Combine(_userContent, fileName);
            using (var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                await stream.CopyToAsync(fs);
            }
            return fileName;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            string originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";

            return await ConfirmSave(file.OpenReadStream(), fileName);
        }
    }
}