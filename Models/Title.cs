namespace Api.Models
{
    public class Title
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Release { get; set; }
        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
        public ICollection<TitleGenre> TitleGenres { get; set; } = new List<TitleGenre>();
    }
}