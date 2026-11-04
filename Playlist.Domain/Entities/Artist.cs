using System;
using System.Collections.Generic;
using System.Text;

namespace PlaylistMigrator.Domain.Entities
{
    public class Artist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public List<ExternalId> ExternalIds { get; set; } = new List<ExternalId>();
    }
}
