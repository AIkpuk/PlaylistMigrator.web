namespace PlaylistMigrator.Infrastructure.DTOs.YandexMusic
{
    public class YandexPlaylistDataDto
    {
        public YandexPlaylistDto Playlist { get; set; } = new();
        public List<YandexTrackDto> Tracks { get; set; } = new();
    }
}