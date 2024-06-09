using Api.Services;

namespace YourProject.Services
{
    public class FileService : IFileService
    {
        private readonly string _path;

        public FileService()
        {
            _path = @"C:\Uploads";
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_path, fileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao enviar o arquivo: " + ex.Message);
            }
        }
    }
}