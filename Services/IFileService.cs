using Microsoft.AspNetCore.Mvc;

namespace Api.Services
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file);
        Task<string> UpdateAsync(string existingFileName, IFormFile newFile);
        Task<FileStreamResult> DownloadAsync(string fileName);
    }
}