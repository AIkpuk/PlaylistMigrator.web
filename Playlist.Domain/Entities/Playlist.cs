namespace PlaylistMigrator.Domain.Entities
{
    public class Playlist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title  { get; set; } = string.Empty;

        public List<ExternalId> ExternalIds { get; set; } = new List<ExternalId>();
        public List<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();
    }
}
