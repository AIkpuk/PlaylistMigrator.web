using PlaylistMigrator.Domain.Entities;

namespace PlaylistMigrator.Application.Interfaces
{
    public interface IDestinationMusicService
    {
        Task<Playlist?> CreatePlaylistAsync(string title, CancellationToken cancellationToken = default);
        Task AddTrackToPlaylistAsync(string playlistId, string trackId, CancellationToken cancellationToken = default);
        Task<Track?> SearchTrackAsync(string trackTitle, List<string> artistNames, CancellationToken cancellationToken = default);
    }
}
