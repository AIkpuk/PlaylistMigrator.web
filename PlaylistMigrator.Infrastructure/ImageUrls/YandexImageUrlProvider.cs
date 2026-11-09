namespace PlaylistMigrator.Infrastructure.ImageUrls
{
    public static class YandexImageUrlProvider
    {
        public static string? GetImageUrl(string? uri, int size)
        {
            if (!string.IsNullOrWhiteSpace(uri))
            {
                if (uri.Contains("%%"))
                {
                    uri = uri.Replace("%%", $"{size}x{size}");
                }
                return uri;
            }
            return null;
        }
    }
}
