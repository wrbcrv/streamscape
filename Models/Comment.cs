namespace Api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime PostedAt { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
    }
}