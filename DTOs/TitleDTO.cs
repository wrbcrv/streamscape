namespace Api.DTOs
{
    public class TitleDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Release { get; set; }
        public List<int> GenreIds { get; set; } = new List<int>();
    }
}