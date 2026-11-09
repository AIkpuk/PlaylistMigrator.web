using PlaylistMigrator.Domain.Entities;
using PlaylistMigrator.Infrastructure.DTOs.YandexMusic;
using PlaylistMigrator.Infrastructure.ImageUrls;

namespace PlaylistMigrator.Infrastructure.Mappings.YandexMusic
{
    public class YandexTrackMapper
    {
        private readonly string _serviceName = "YandexMusic";
        private readonly YandexAlbumMapper _albumMapper;
        private readonly YandexArtistMapper _artistMapper;

        public YandexTrackMapper(YandexAlbumMapper albumMapper, YandexArtistMapper artistMapper)
        {
            _albumMapper = albumMapper;
            _artistMapper = artistMapper;
        }

        public Track Map(YandexTrackDto yandexTrackDto)
        {
            var albumDto = yandexTrackDto.Albums?.FirstOrDefault();
            var album = albumDto != null ? _albumMapper.Map(albumDto) : null;

            var artists =  yandexTrackDto.Artists?.Select(_artistMapper.Map).ToList() ?? new List<Artist>();

            Track track = new Track
            {
                Id = Guid.NewGuid(),
                Title = yandexTrackDto.Title,
                Duration = TimeSpan.FromMilliseconds(yandexTrackDto.DurationMs ?? 0),
                
                Album = album,  
                AlbumId = album?.Id,

                Artists = artists,
                ExternalIds = new List<ExternalId>
                {
                    new ExternalId
                    {
                        Service = _serviceName,
                        Value = yandexTrackDto.Id.ToString(),
                    }
                },
                CoverUri = ImageUrlService.GetImageUrl(_serviceName, yandexTrackDto.CoverUri,100)

            };
            return track;
        }
    }
}
