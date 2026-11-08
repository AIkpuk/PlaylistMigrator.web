using System;
using System.Collections.Generic;
using System.Text;

namespace PlaylistMigrator.Domain.Entities
{
    public class Album
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string? CoverUri { get; set; }
        public List<Artist> Artists { get; set; } = new();
        public List<ExternalId> ExternalIds { get; set; } = new List<ExternalId>();
    }
}
