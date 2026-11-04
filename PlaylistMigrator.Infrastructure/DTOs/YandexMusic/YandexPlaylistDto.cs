using System.Text.Json.Serialization;

namespace PlaylistMigrator.Infrastructure.DTOs.YandexMusic
{
    public class YandexPlaylistDto
    {
        [JsonPropertyName("uid")]
        public long Uid { get; set; }
        [JsonPropertyName("kind")]
        public long Kind { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("trackCount")]
        public int TrackCount { get; set; }
        [JsonPropertyName("tracks")]
        public List<YandexPlaylistTrackDto> Tracks { get; set; } = new();
    }
}
