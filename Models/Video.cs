namespace StreamingAPI.Models
{
    public class Video
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Url { get; set; }
        public int PlaylistId { get; set; }
        public required Playlist Playlist { get; set; }

    }
}
