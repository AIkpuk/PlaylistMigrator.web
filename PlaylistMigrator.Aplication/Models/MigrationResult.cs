namespace PlaylistMigrator.Application.Models
{
    public class MigrationResult
    {
        public int TotalTracks { get; set; }
        public int MigratedTracks { get; set; }
        public int FailedTracks { get; set; }
        public int NotFoundTracks { get; set; }
    }
}
