using System.Text.Json.Serialization;

namespace PlaylistMigrator.Infrastructure.DTOs.YandexMusic
{
    public class YandexAlbumDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("title")] 
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("coverUri")]
        public string? CoverUri { get; set; }
        [JsonPropertyName("year")]
        public int? Year { get; set; }
    }
}
