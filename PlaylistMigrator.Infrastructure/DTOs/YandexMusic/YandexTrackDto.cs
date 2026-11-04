using System.Text.Json.Serialization;

namespace PlaylistMigrator.Infrastructure.DTOs.YandexMusic
{
    public class YandexTrackDto
    {
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("durationMs")]
        public long? DurationMs { get; set; }
        [JsonPropertyName("artists")]
        public List<YandexArtistDto> Artists { get; set; } = new();
        [JsonPropertyName("albums")]
        public List<YandexAlbumDto> Albums { get; set; } = new();
    }
}
