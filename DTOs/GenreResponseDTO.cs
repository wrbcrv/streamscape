using Api.Models;
using Api.Services;

namespace Api.DTOs
{
    public class GenreResponseDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public static GenreResponseDTO ValueOf(Genre genre)
        {
            return new GenreResponseDTO
            {
                Id = genre.Id,
                Type = genre.Type.GetDescription(),
            };
        }
    }
}