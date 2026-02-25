using static System.Net.Mime.MediaTypeNames;

namespace GameZone.Settings
{
    public static class FileSettings
    {
        public const string ImagesPath = "/assets/images/games";
        public const string AllowExtentions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMB =2;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}
