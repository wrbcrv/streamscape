using Api.Models;

namespace Api.DTOs
{
    public class EpisodeResponseDTO
    {
        public int Id { get; set; }
        public string Source { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public static EpisodeResponseDTO ValueOf(Episode episode)
        {
            return new EpisodeResponseDTO
            {
                Id = episode.Id,
                Source = episode.Source,
                Number = episode.Number,
                Name = episode.Name,
                Description = episode.Description,
            };
        }
    }
}