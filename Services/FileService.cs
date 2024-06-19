using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

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

        public async Task<string> UpdateAsync(string existingFileName, IFormFile newFile)
        {
            if (newFile == null || newFile.Length == 0)
            {
                throw new ArgumentException("Nenhum arquivo fornecido para atualização.");
            }

            var existingFilePath = Path.Combine(_path, existingFileName);

            if (!File.Exists(existingFilePath))
            {
                throw new FileNotFoundException("Arquivo a ser atualizado não encontrado.");
            }

            var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(newFile.FileName);
            var newFilePath = Path.Combine(_path, newFileName);

            try
            {
                File.Delete(existingFilePath);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await newFile.CopyToAsync(stream);
                }

                return newFileName;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao atualizar o arquivo: " + ex.Message);
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
