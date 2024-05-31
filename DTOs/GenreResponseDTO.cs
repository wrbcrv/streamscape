using Api.Models;

namespace Api.DTOs
{
    public class GenreResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public static GenreResponseDTO ValueOf(Genre genre)
        {
            return new GenreResponseDTO
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }
    }
}