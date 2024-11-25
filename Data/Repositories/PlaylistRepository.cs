using Microsoft.EntityFrameworkCore;
using StreamingAPI.Models;

namespace StreamingAPI.Data.Repositories
{
    public class PlaylistRepository
    {
        private readonly StreamingContext _context;

        public PlaylistRepository(StreamingContext context)
        {
            _context = context;
        }

        public List<Playlist> GetAllPlaylists()
        {
            return _context.Playlists.ToList();
        }

        public Playlist GetPlaylistById(int id)
        {
            return _context.Playlists.Find(id);
        }

        public void AddPlaylist(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            _context.SaveChanges();
        }

        public void UpdatePlaylist(Playlist playlist)
        {
            _context.Playlists.Update(playlist);
            _context.SaveChanges();
        }

        public void DeletePlaylist(int id)
        {
            var playlist = _context.Playlists.Find(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
                _context.SaveChanges();
            }
        }
    }
}
