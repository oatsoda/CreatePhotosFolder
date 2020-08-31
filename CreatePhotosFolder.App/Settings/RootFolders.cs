using System;
using System.Collections.Generic;

namespace CreatePhotosFolder.App.Settings
{
    public static class RootFolders
    {

        public static List<RootFolder> LoadPhotoRootFolders()
        {
            var rootFolders = new List<RootFolder>
                              {
                                  new RootFolder("Pictures", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures))
                              };

            if (AppSettings.Settings.Value.HasCustomRootFolderPath)
                rootFolders.Add(new RootFolder(AppSettings.Settings.Value.CustomRootFolderPath, AppSettings.Settings.Value.CustomRootFolderPath));

            return rootFolders;
        }
    }
}