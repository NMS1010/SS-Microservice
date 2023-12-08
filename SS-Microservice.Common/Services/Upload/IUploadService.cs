using Microsoft.AspNetCore.Http;

namespace SS_Microservice.Common.Services.Upload
{
    public interface IUploadService
    {
        Task<string> UploadFile(IFormFile file);

        void DeleteFile(string filename);
    }
}