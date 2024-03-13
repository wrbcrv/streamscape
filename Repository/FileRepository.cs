using api.Interfaces;

namespace api.Repository
{
    public class FileRepository : IFileRepository
    {
        public async Task<string> SaveFileAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty.");
            }

            if (!IsImage(file.FileName))
            {
                throw new ArgumentException("Only image files are allowed.");
            }

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string unique = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(folder, unique);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }

        public bool IsImage(string fileName)
        {
            string[] allowed = [".jpg", ".jpeg", ".png", ".gif"];
            string extension = Path.GetExtension(fileName).ToLower();
            
            return Array.IndexOf(allowed, extension) != -1;
        }
        
        public byte[] DownloadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found. " + filePath);
            }

            return File.ReadAllBytes(filePath);
        }
    }
}