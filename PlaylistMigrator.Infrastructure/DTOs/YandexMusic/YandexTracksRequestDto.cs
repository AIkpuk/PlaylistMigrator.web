using System.Text.Json.Serialization;

namespace PlaylistMigrator.Infrastructure.DTOs.YandexMusic
{
    public class YandexTracksRequestDto
    {
        [JsonPropertyName("trackIds")]
        public List<string> TrackIds { get; set; } = new();

    }
}
