namespace PlaylistMigrator.Domain.Entities
{
    public class Track
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; } 
        public Guid? AlbumId { get; set; }
        public Album? Album { get; set; }
        public string? CoverUri { get; set; }
        public List<Artist> Artists { get; set; } = new();
        public List<ExternalId> ExternalIds { get; set; } = new();

    }
}
