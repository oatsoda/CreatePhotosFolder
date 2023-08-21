using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatePhotosFolder.App.Settings
{
    public static class UserSettings
    {
        internal static Properties.Settings Settings => Properties.Settings.Default;

        private static List<string> m_ImageExtensions = null;
        private static List<string> m_AdditionalRootFolderPaths = null;

        public static void UpgradeIfRequired()
        {
            // Settings upgrade dance for newer versions of the program
            if (!Properties.Settings.Default.RequiresUpgrade)
                return;

            Properties.Settings.Default.Upgrade();
            Properties.Settings.Default.RequiresUpgrade = false;
            Properties.Settings.Default.Save();
        }

        public static bool IsImageFile(this string fileExtension) 
        {
            if (m_ImageExtensions == null)
                m_ImageExtensions = Settings.ImageExtensions
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(e => e.Trim().ToLowerInvariant())
                    .ToList();

            return m_ImageExtensions.Contains(fileExtension.ToLowerInvariant());
        }

        public static IReadOnlyList<string> AdditionalRootFolderPaths()
        {            
            if (m_AdditionalRootFolderPaths == null)
                m_AdditionalRootFolderPaths = Settings.AdditionalRootFolderPaths
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(e => e.Trim())
                    .ToList();

            return m_AdditionalRootFolderPaths;
        }
    }
}