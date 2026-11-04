using PlaylistMigrator.Domain.Entities;
using PlaylistMigrator.Infrastructure.DTOs.YandexMusic;

namespace PlaylistMigrator.Infrastructure.Mappings.YandexMusic
{
    public class YandexArtistMapper
    {
        public Artist Map(YandexArtistDto yandexArtistDto)
        {
            Artist artist = new Artist
            {
                Id = Guid.NewGuid(),
                ExternalIds = new List<ExternalId>
                {
                    new ExternalId
                    {
                        Service = "YandexMusic",
                        Value = yandexArtistDto.Id.ToString(),
                    }
                },
                Name = yandexArtistDto.Name
            };

            return artist;
        }
    }
}
