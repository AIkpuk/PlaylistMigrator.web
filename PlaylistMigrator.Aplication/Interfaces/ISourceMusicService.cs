using PlaylistMigrator.Domain.Entities;

namespace PlaylistMigrator.Application.Interfaces
{
    public interface ISourceMusicService
    {
        Task<Playlist?> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken = default);

        Task<List<Playlist>> GetUserPlaylistsAsync(CancellationToken cancellationToken = default);
    }
}
