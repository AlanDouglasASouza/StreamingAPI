using System.Text.Json.Serialization;

namespace StreamingAPI.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        
        public List<Content> Contents { get; set; } = new();
    }
}

