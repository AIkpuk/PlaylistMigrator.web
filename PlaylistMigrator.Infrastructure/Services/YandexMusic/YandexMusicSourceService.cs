using PlaylistMigrator.Application.Interfaces;
using PlaylistMigrator.Domain.Entities;
using PlaylistMigrator.Infrastructure.Clients;
using PlaylistMigrator.Infrastructure.Mappings.YandexMusic;

namespace PlaylistMigrator.Infrastructure.Services.YandexMusic
{
    public class YandexMusicSourceService : ISourceMusicService
    {
        private readonly YandexMusicClient _yandexMusicClient;
        private readonly YandexPlaylistMapper _yandexPlaylistMapper;
        public YandexMusicSourceService(YandexMusicClient yandexMusicClient, YandexPlaylistMapper yandexPlaylistMapper)
        {
            _yandexMusicClient = yandexMusicClient;
            _yandexPlaylistMapper = yandexPlaylistMapper;
        }
        public async Task<Playlist?> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(playlistId))
            {
                var yandexPlaylistDto = await _yandexMusicClient.GetPlaylistAsync(playlistId, cancellationToken);
                if (yandexPlaylistDto != null)
                {
                    return _yandexPlaylistMapper.Map(yandexPlaylistDto);
                }
                return null;
            }
            throw new ArgumentException("Invalid playlist ID format");
        }

        public Task<List<Playlist>> GetUserPlaylistsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
