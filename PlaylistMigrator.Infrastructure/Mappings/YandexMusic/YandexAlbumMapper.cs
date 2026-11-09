using PlaylistMigrator.Domain.Entities;
using PlaylistMigrator.Infrastructure.DTOs.YandexMusic;
using PlaylistMigrator.Infrastructure.ImageUrls;

namespace PlaylistMigrator.Infrastructure.Mappings.YandexMusic
{
    public class YandexAlbumMapper
    {
        private readonly string _serviceName = "YandexMusic";
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
                        Service = _serviceName,
                        Value = yandexAlbumDto.Id.ToString(),
                    }
                },
                Artists = new List<Artist>(),
                CoverUri = ImageUrlService.GetImageUrl(_serviceName, yandexAlbumDto.CoverUri, 100)
            };

            return album;
        }
    }
}
