using Api.Models;

namespace Api.DTOs
{
    public class EpisodeResponseDTO
    {
        public int Id { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public static EpisodeResponseDTO ValueOf(Episode episode)
        {
            return new EpisodeResponseDTO
            {
                Id = episode.Id,
                Source = episode.Source,
                Name = episode.Name
            };
        }
    }
}