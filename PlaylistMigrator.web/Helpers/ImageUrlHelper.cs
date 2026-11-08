namespace PlaylistMigrator.web.Helpers
{
    public static class ImageUrlHelper
    {
        public static string? GetImageUrl(string? uri)
        {
            if (!string.IsNullOrWhiteSpace(uri))
            {
                if(!uri.StartsWith("https://", StringComparison.OrdinalIgnoreCase) && !uri.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                {
                    uri = "https://" + uri;
                }
                
                if (uri.Contains("%%"))
                {
                    uri = uri.Replace("%%", "100x100");
                }
                
                return uri;
            }
            return null;
        }
    }
}
