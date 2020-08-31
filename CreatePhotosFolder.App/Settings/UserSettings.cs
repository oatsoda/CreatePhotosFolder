namespace CreatePhotosFolder.App.Settings
{
    public static class UserSettings
    {
        internal static Properties.Settings Settings => Properties.Settings.Default;

        public static void UpgradeIfRequired()
        {
            // Settings upgrade dance for newer versions of the program
            if (!Properties.Settings.Default.RequiresUpgrade)
                return;

            Properties.Settings.Default.Upgrade();
            Properties.Settings.Default.RequiresUpgrade = false;
            Properties.Settings.Default.Save();
        }
    }
}