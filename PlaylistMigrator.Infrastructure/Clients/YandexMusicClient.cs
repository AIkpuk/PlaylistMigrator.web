using PlaylistMigrator.Infrastructure.DTOs.YandexMusic;
using System.Net.Http.Json;

namespace PlaylistMigrator.Infrastructure.Clients
{
    public class YandexMusicClient
    {
        private readonly HttpClient _httpClient;

        public YandexMusicClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<YandexPlaylistDto?> GetPlaylistAsync(string playlistUuid, CancellationToken cancellationToken = default)
        {
            var playlist = await _httpClient.GetFromJsonAsync<YandexPlaylistDto?>($"playlist/{playlistUuid}?resumeStream=false&richTracks=false", cancellationToken);

            if (playlist != null)
            {
                return playlist;
            }
            throw new InvalidOperationException("The playlist is empty.");
        }

        public async Task<List<YandexTrackDto>> GetTracksAsync(List<string> trackIds, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("tracks", new YandexTracksRequestDto { TrackIds = trackIds }, cancellationToken);
            response.EnsureSuccessStatusCode();

            var tracks = await response.Content.ReadFromJsonAsync<List<YandexTrackDto>>(cancellationToken: cancellationToken);
            if (tracks != null)
            {
                return tracks;
            }

            throw new InvalidOperationException("Failed to retrieve tracks.");
        }

        public async Task<List<YandexTrackDto>> GetPlaylistTracksAsync(string playlistUuid,CancellationToken cancellationToken = default)
        {
            YandexPlaylistDto? playlist = await GetPlaylistAsync(playlistUuid, cancellationToken);

            var trackIds = playlist.Tracks
                .Where(t => t.AlbumId != null)
                .Select(x => $"{x.Id}:{x.AlbumId}")
                .ToList();

            if (trackIds.Count == 0)
            {
                throw new InvalidOperationException("Playlist contains no valid track IDs.");
            }

            var tracks = await GetTracksAsync(trackIds, cancellationToken);

            return tracks;
        }


    }
}