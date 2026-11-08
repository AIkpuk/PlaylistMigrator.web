using PlaylistMigrator.Domain.Entities;
using PlaylistMigrator.Infrastructure.DTOs.YandexMusic;

namespace PlaylistMigrator.Infrastructure.Mappings.YandexMusic
{
    public class YandexPlaylistMapper
    {
        private readonly YandexTrackMapper _yandexTrackMapper;

        public YandexPlaylistMapper(YandexTrackMapper yandexTrackMapper)
        {
            _yandexTrackMapper = yandexTrackMapper;
        }

        public Playlist Map(
            YandexPlaylistDto yandexPlaylistDto,
            List<YandexTrackDto>? tracks)
        {
            Playlist playlist = new Playlist
            {
                Id = Guid.NewGuid(),
                Title = yandexPlaylistDto.Title,
                ExternalIds = new List<ExternalId>
                {
                    new ExternalId
                    {
                        Service = "YandexMusic",
                        Value = $"{yandexPlaylistDto.Uid}:{yandexPlaylistDto.Kind}"
                    }
                },
                OgImage = yandexPlaylistDto.OgImage,
                PlaylistTracks = new List<PlaylistTrack>()
            };

            if (tracks == null || yandexPlaylistDto.Tracks == null)
                return playlist;

            var playlistTracks = yandexPlaylistDto.Tracks
                .Select((item, index) =>
                {
                    YandexTrackDto? yandexTrack = tracks.FirstOrDefault(track =>
                        track.Id == item.Id &&
                        track.Albums.Any(album => album.Id == item.AlbumId));

                    if (yandexTrack == null)
                        return null;

                    Track track = _yandexTrackMapper.Map(yandexTrack);

                    return new PlaylistTrack
                    {
                        PlaylistId = playlist.Id,
                        Playlist = playlist,

                        TrackId = track.Id,
                        Track = track,

                        SourcePosition = item.OriginalIndex ?? index,
                        DestinationPosition = null
                    };
                })
                .Where(item => item != null)
                .ToList();

            playlist.PlaylistTracks = playlistTracks!;

            return playlist;
        }
    }
}