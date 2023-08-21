using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CreatePhotosFolder.App.Settings;

namespace CreatePhotosFolder.App.Extensions
{
    public static class FileInfoImageExtensions
    {
        private static readonly Regex s_Regex = new Regex(":");

        public static bool GetDateTakenFromImage(this FileInfo image, out DateTime dateTaken)
        {
            //retrieves the datetime WITHOUT loading the whole image
            try
            {
                using (var fs = new FileStream(image.FullName, FileMode.Open, FileAccess.Read))
                using (var myImage = Image.FromStream(fs, false, false))
                {
                    var propItem = myImage.GetPropertyItem(36867);
                    var dateTakenString = s_Regex.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.TryParse(dateTakenString, out dateTaken);
                }
            }
            catch (ArgumentException ex)
            {
                Trace.WriteLine($"Exception getting Date Taken: {ex}");
                dateTaken = DateTime.MinValue;
                return false;
            }
        }

        public static bool IsImageFile(this FileInfo fileInfo) => UserSettings.IsImageFile(fileInfo.Extension);
    }
}