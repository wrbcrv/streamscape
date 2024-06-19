using System.Collections.Generic;

namespace Api.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public string Source { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TitleId { get; set; }
        public Title? Title { get; set; }
    }
}