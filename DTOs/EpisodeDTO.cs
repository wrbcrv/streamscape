namespace Api.DTOs
{
    public class EpisodeDTO
    {
        public string Source { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}