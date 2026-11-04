using System.Text.Json.Serialization;

namespace PlaylistMigrator.Infrastructure.DTOs.YandexMusic
{
    public class YandexAlbumDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("title")] 
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("year")]
        public int? Year { get; set; }
    }
}
