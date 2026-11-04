using System.Text.Json.Serialization;

namespace PlaylistMigrator.Infrastructure.DTOs.YandexMusic
{
    public class YandexApiResponse<T>
    {
        [JsonPropertyName("result")]
        public T? Result { get; set; }
    }
}