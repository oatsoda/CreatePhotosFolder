using System;
using System.Configuration;
using System.Linq;

namespace CreatePhotosFolder.App.Settings
{
    public class AppSettings
    {
        public static Lazy<AppSettings> Settings = new Lazy<AppSettings>(() => new AppSettings());
        
        public string CustomRootFolderPath { get; }
        public string[] ImageExtensions { get; }

        public bool HasCustomRootFolderPath => !string.IsNullOrWhiteSpace(CustomRootFolderPath);

        private AppSettings()
        {
            CustomRootFolderPath = ConfigurationManager.AppSettings["CustomPath"];
            ImageExtensions = new [] { ".jpg", ".gif", ".png" }; // Assume always lowercase
        }

        public bool IsImageFile(string fileExtension) => ImageExtensions.Contains(fileExtension.ToLowerInvariant());
    }
}