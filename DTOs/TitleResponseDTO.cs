using Api.Models;

namespace Api.DTOs
{
    public class TitleResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Release { get; set; }
        public List<EpisodeResponseDTO>? Episodes { get; set; }
        public List<GenreResponseDTO>? Genres { get; set; }

        public static TitleResponseDTO ValueOf(Title title)
        {
            return new TitleResponseDTO
            {
                Id = title.Id,
                Name = title.Name,
                Description = title.Description,
                Release = title.Release,
                Episodes = title.Episodes?.Select(EpisodeResponseDTO.ValueOf).ToList(),
                Genres = title.TitleGenres?.Select(tg => GenreResponseDTO.ValueOf(tg.Genre)).ToList()
            };
        }
    }
}