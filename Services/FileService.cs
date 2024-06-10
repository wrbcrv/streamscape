using Api.Services;
using Microsoft.AspNetCore.Mvc;

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
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Nenhum arquivo fornecido para upload.");
            }

            if (!file.ContentType.StartsWith("video/"))
            {
                throw new ArgumentException("O arquivo fornecido não é um vídeo.");
            }

            long size = 1024 * 1024 * 1024;
            
            if (file.Length > size)
            {
                throw new ArgumentException("O tamanho do arquivo excede o limite permitido.");
            }

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

        public async Task<FileStreamResult> DownloadAsync(string fileName)
        {
            var filePath = Path.Combine(_path, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Arquivo não encontrado.");
            }

            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;

            var fileStreamResult = new FileStreamResult(memoryStream, "application/octet-stream")
            {
                FileDownloadName = fileName
            };

            return fileStreamResult;
        }
    }
}