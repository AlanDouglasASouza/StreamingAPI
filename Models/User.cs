using System.Text.Json.Serialization;

namespace StreamingAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        [JsonIgnore]
        public required string Password { get; set; }
        public List<Playlist> Playlists { get; set; } = new();
    }
}

