using System.Text.Json.Serialization;

namespace PlaylistMigrator.Infrastructure.DTOs.YandexMusic
{
    public class YandexPlaylistTrackDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("originalIndex")]
        public int? OriginalIndex { get; set; }
        [JsonPropertyName("albumId")]
        public long? AlbumId { get; set; }
    }
}
