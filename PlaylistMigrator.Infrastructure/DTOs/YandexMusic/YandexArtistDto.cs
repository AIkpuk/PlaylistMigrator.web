using System.Text.Json.Serialization;

namespace PlaylistMigrator.Infrastructure.DTOs.YandexMusic
{
    public class YandexArtistDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
