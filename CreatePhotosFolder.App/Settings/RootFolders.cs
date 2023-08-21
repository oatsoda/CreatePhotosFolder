using System;
using System.Collections.Generic;
using System.Linq;

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

            if (UserSettings.AdditionalRootFolderPaths().Count > 0)
                rootFolders.AddRange(UserSettings.AdditionalRootFolderPaths().Select(p => new RootFolder(p, p)));

            return rootFolders;
        }
    }
}