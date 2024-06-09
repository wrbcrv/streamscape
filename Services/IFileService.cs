namespace Api.Services
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}