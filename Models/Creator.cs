namespace StreamingAPI.Models
{
    public class Creator
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Content> Contents { get; set; } = new();
    }
}

