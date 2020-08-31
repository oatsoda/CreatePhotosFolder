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

        public static DateTime GetDateTakenFromImage(this FileInfo image)
        {
            //retrieves the datetime WITHOUT loading the whole image
            try
            {
                using (var fs = new FileStream(image.FullName, FileMode.Open, FileAccess.Read))
                using (var myImage = Image.FromStream(fs, false, false))
                {
                    var propItem = myImage.GetPropertyItem(36867);
                    var dateTaken = s_Regex.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.Parse(dateTaken);
                }
            }
            catch (ArgumentException ex)
            {
                Trace.WriteLine($"Exception getting Date Taken: {ex}");
                return image.CreationTime;
            }
        }

        public static bool IsImageFile(this FileInfo fileInfo)
        {
            return AppSettings.Settings.Value.IsImageFile(fileInfo.Extension);
        }
    }
}