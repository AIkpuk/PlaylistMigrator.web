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
            var response = await _httpClient.GetFromJsonAsync<YandexApiResponse<YandexPlaylistDto>>(
                $"playlist/{playlistUuid}?resumeStream=false&richTracks=false",
                cancellationToken);

            if (response?.Result != null)
            {
                return response.Result;
            }

            throw new InvalidOperationException("Failed to retrieve playlist.");
        }

        public async Task<List<YandexTrackDto>> GetTracksAsync(List<string> trackIds, CancellationToken cancellationToken = default)
        {

            var content = new FormUrlEncodedContent(
                trackIds.Select(id =>
                    new KeyValuePair<string, string>("trackIds", id))
            );

            var response = await _httpClient.PostAsync(
                "tracks",
                content,
                cancellationToken);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            var apiResponse = await response.Content
                .ReadFromJsonAsync<YandexApiResponse<List<YandexTrackDto>>>(
                    cancellationToken: cancellationToken);

            if (apiResponse?.Result != null)
            {
                return apiResponse.Result;
            }

            throw new InvalidOperationException("Failed to retrieve tracks.");
        }

        public async Task<YandexPlaylistDataDto> GetPlaylistDataAsync(string playlistUuid,CancellationToken cancellationToken = default)
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

            return new YandexPlaylistDataDto
            {
                Playlist = playlist,
                Tracks = tracks
            };
        }


    }
}