using PlaylistMigrator.Application.Interfaces;
using PlaylistMigrator.Application.Models;

namespace PlaylistMigrator.Application.Services
{
    public class MigrationService
    {
        private readonly ISourceMusicService _sourceMusicService;
        private readonly IDestinationMusicService _destinationMusicService;

        public MigrationService(ISourceMusicService sourceMusicService, IDestinationMusicService destinationMusicService)
        {
            _sourceMusicService = sourceMusicService;
            _destinationMusicService = destinationMusicService;
        }
        public async Task<MigrationResult> MigratePlaylistAsync(string sourcePlaylistId, string destinationPlaylistName, CancellationToken cancellationToken = default)
        {
            var sourcePlaylist = await _sourceMusicService.GetPlaylistAsync(sourcePlaylistId, cancellationToken);
            if (sourcePlaylist == null)
            {
                throw new InvalidOperationException($"Playlist with ID {sourcePlaylistId} was not found");
            }

            var destinationPlaylist = await _destinationMusicService.CreatePlaylistAsync(destinationPlaylistName, cancellationToken);
            if (destinationPlaylist == null)
            { 
                throw new InvalidOperationException($"Failed to create destination playlist");
            }

            var migrationResult = new MigrationResult
            {
                TotalTracks = sourcePlaylist.PlaylistTracks.Count
            };

            foreach (var playlistTrack in sourcePlaylist.PlaylistTracks)
            {
                var track = playlistTrack.Track;
                
                if (track == null)
                {
                    migrationResult.NotFoundTracks++;
                    continue;
                }
                var artistNames = track.Artists.Select(a => a.Name).ToList();
                var destinationTrack = await _destinationMusicService.SearchTrackAsync(track.Title, artistNames, cancellationToken);
                if (destinationTrack == null)
                {
                    migrationResult.NotFoundTracks++;
                    continue;
                }

                try
                {
                    await _destinationMusicService.AddTrackToPlaylistAsync(destinationPlaylist.Id.ToString(), destinationTrack.Id.ToString(), cancellationToken);
                    migrationResult.MigratedTracks++;
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception)
                {
                    migrationResult.FailedTracks++;
                }
            }

            if (migrationResult.TotalTracks != migrationResult.MigratedTracks + migrationResult.FailedTracks + migrationResult.NotFoundTracks)
            {
                throw new InvalidOperationException("Migration result is inconsistent");
            }

            return migrationResult;
        }
    }
}
