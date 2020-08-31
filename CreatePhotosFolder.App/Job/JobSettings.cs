using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CreatePhotosFolder.App.Extensions;
using CreatePhotosFolder.App.Settings;

namespace CreatePhotosFolder.App.Job
{
    public class JobSettings
    {
        public RootFolder RootFolder { get; set; }
        public string AlbumFolderName { get; set; }
        public bool AddDatesToFolderName { get; set; }
        
        public List<FileInfo> RequestedFiles { get; }
        public Action<ProgressUpdateEventArgs> UpdateAction { get; }

        public int TotalFiles => RequestedFiles.Count;
        public int TotalImageFiles => RequestedFiles.Count(f => f.IsImageFile());
        public string DestinationFolder => Path.Combine(RootFolder.Path, AlbumFolderName);

        public JobSettings(IEnumerable<string> fileNames, Action<ProgressUpdateEventArgs> updateAction)
        {
            // TODO: validate param not null
            // TODO: any validation? does FileInfo ctor throw?
            RequestedFiles = fileNames.Select(f => new FileInfo(f)).ToList();

            UpdateAction = updateAction;
        }

        public bool SettingsValid => RootFolder != null &&
                                     !string.IsNullOrWhiteSpace(AlbumFolderName) &&
                                     UpdateAction != null;
    }
}