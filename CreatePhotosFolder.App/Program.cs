using System;
using System.Windows.Forms;
using CreatePhotosFolder.App.Settings;

namespace CreatePhotosFolder.App
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(params string[] args)
        {
            UserSettings.UpgradeIfRequired();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CreatePhotosFolderForm(args));
        }
    }
}
