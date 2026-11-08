using PlaylistMigrator.Domain.Entities;
using PlaylistMigrator.Infrastructure.DTOs.YandexMusic;

namespace PlaylistMigrator.Infrastructure.Mappings.YandexMusic
{
    public class YandexAlbumMapper
    {
        public Album Map(YandexAlbumDto yandexAlbumDto)
        {
            Album album = new Album
            {
                Id = Guid.NewGuid(),
                Title = yandexAlbumDto.Title,
                ExternalIds = new List<ExternalId>
                {
                    new ExternalId
                    {
                        Service = "YandexMusic",
                        Value = yandexAlbumDto.Id.ToString(),
                    }
                },
                Artists = new List<Artist>(),
                CoverUri = yandexAlbumDto.CoverUri
            };

            return album;
        }
    }
}
