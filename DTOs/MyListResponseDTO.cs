using Api.Models;

namespace Api.DTOs
{
    public class MyListResponseDTO
    {
        public int Id { get; set; }
        public string Thumbnail { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public static MyListResponseDTO ValueOf(Title title)
        {
            return new MyListResponseDTO
            {
                Id = title.Id,
                Name = title.Name,
                Thumbnail = title.Thumbnail
            };
        }
    }
}
