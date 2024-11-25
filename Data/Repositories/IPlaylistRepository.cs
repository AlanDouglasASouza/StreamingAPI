using StreamingAPI.Models;

namespace StreamingAPI.Data.Repositories
{
    public interface IPlaylistRepository
    {
        Task<List<Playlist>> GetAllAsync();
        Task<Playlist> GetByIdAsync(int id);
        Task AddAsync(Playlist playlist);
        Task UpdateAsync(Playlist playlist);
        Task DeleteAsync(int id);

    }
}
