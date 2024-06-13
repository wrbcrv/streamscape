namespace Api.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public Type Type { get; set; }
        public ICollection<TitleGenre> TitleGenres { get; set; } = new List<TitleGenre>();
    }
}