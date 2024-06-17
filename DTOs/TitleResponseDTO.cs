using Api.Models;
using Api.Services;

namespace Api.DTOs
{
    public class TitleResponseDTO
    {
        public int Id { get; set; }
        public string Thumbnail { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Release { get; set; }
        public string Rating { get; set; } = string.Empty;
        public List<EpisodeResponseDTO>? Episodes { get; set; }
        public List<GenreResponseDTO>? Genres { get; set; }

        public static TitleResponseDTO ValueOf(Title title)
        {
            return new TitleResponseDTO
            {
                Id = title.Id,
                Thumbnail = title.Thumbnail,
                Name = title.Name,
                Description = title.Description,
                Release = title.Release,
                Rating = title.Rating.GetDescription(),
                Episodes = title.Episodes?.Select(EpisodeResponseDTO.ValueOf).ToList(),
                Genres = title.TitleGenres?.Select(tg => GenreResponseDTO.ValueOf(tg.Genre)).ToList()
            };
        }
    }
}