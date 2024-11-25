namespace StreamingAPI.Models
{
    public class Content
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Type { get; set; }
        public required Creator Creator { get; set; }
    }
}
