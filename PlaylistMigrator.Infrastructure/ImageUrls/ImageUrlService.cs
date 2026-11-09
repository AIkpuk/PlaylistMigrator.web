namespace PlaylistMigrator.Infrastructure.ImageUrls
{
    public static class ImageUrlService
    {
        public static string? GetImageUrl(string? service, string? uri, int size)
        {
            if (size < 1)
            {
                throw new ArgumentException($"Invalid image size: {size}", nameof(size));
            }

            switch (service)
            {
                case "YandexMusic":
                    return YandexImageUrlProvider.GetImageUrl(uri, size);


                default:
                    return null;
            }
        }
    }
}
