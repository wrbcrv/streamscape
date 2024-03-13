
namespace api.Interfaces
{
    public interface IFileRepository
    {
        Task<string> SaveFileAsync(IFormFile file, string folder);
        bool IsImage(string fileName);

        byte[] DownloadFile(string filePath);
    }
}