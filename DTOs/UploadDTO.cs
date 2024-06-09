namespace Api.DTOs
{
    public class UploadDTO
    {
        public string  Name { get; set; } = string.Empty;
        public IFormFile? File { get; set; }
    }
}