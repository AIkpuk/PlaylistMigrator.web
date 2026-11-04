using PlaylistMigrator.Domain.Entities;
using PlaylistMigrator.Infrastructure.DTOs.YandexMusic;

namespace PlaylistMigrator.Infrastructure.Mappings.YandexMusic
{
    public class YandexTrackMapper
    {
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

            var artistDtos = yandexTrackDto.Artists;
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
                        Service = "YandexMusic",
                        Value = yandexTrackDto.Id.ToString(),
                    }
                }
            };
            return track;
        }
    }
}
