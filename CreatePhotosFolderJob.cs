using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CreatePhotosFolder.Extensions;

namespace CreatePhotosFolder
{
    public class CreatePhotosFolderJob
    {
        private readonly List<FileInfo> m_RequestedFiles;
        private JobResult m_JobResult;

        private string m_CurrentOperation;

        public int TotalFiles => m_RequestedFiles.Count;

        public int TotalImageFiles => m_RequestedFiles.Count(IsImageFile);

        public ParentFolder ParentFolder { get; set; }
        public string AlbumFolderName { get; set; }
        public bool AddDatesToFolderName { get; set; }

        public event EventHandler<ProgressUpdateEventArgs> ProgressUpdate;

        private string DestinationFolder => Path.Combine(ParentFolder.Path, AlbumFolderName);

        public CreatePhotosFolderJob(string[] filenames)
        {
            // TODO: validate param not null
            // TODO: any validation? does fileinfo ctor throw?
            m_RequestedFiles = filenames.Select(f => new FileInfo(f)).ToList();
        }

        private bool IsImageFile(FileInfo fileInfo)
        {
            return string.Compare(fileInfo.Extension, ".jpg", StringComparison.InvariantCultureIgnoreCase) == 0 ||
                   string.Compare(fileInfo.Extension, ".gif", StringComparison.InvariantCultureIgnoreCase) == 0 ||
                   string.Compare(fileInfo.Extension, ".png", StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public async Task<JobResult> MoveFiles()
        {
            return await Task.Run(() => MoveFilesInternal());
        }

        private async Task<JobResult> MoveFilesInternal()
        {
            m_JobResult = new JobResult();
            
            m_CurrentOperation = "Validating files";
            OnProgress(0, m_CurrentOperation);

            await PrepareFiles();

            if (!m_JobResult.Success)
                return m_JobResult;

            m_CurrentOperation = "Copying files to new location";
            OnProgress(0, m_CurrentOperation);

            await CopyFiles();

            if (!m_JobResult.Success)
                return m_JobResult;

            m_CurrentOperation = "Verifying copied files";
            OnProgress(0, m_CurrentOperation);

            await VerifyCopiedFiles();
            
            if (!m_JobResult.Success)
                return m_JobResult;

            m_CurrentOperation = "Deleting source files";
            OnProgress(0, m_CurrentOperation);

            await DeleteSourceFiles();

            return m_JobResult;
        }

        private async Task PrepareFiles()
        {
            var failures = new List<string>();

            var minDate = DateTime.MaxValue;
            var maxDate = DateTime.MinValue;


            var c = 0;
            foreach (var file in m_RequestedFiles)
            {
                if (!file.Exists)
                {
                    failures.Add($"File does not exist: {file.FullName}");
                }
                else
                {
                    if (AddDatesToFolderName)
                    { 
                        var dateTaken = GetDateTakenFromImage(file);

                        if (file.CreationTime < minDate)
                            minDate = dateTaken;

                        if (file.CreationTime > maxDate)
                            maxDate = dateTaken;
                    }
                }

                c++; 
                OnProgress(Percentage(c), m_CurrentOperation);
            }

            if (AddDatesToFolderName)
            {
                var minDateStr = minDate.ToString("yyyy.MM.dd");
                var maxDateStr = maxDate.ToString("yyyy.MM.dd");
                if (maxDateStr == minDateStr)
                    maxDateStr = "";
                var newFolderName = $"{minDateStr} {AlbumFolderName} {maxDateStr}".TrimEnd();
                AlbumFolderName = newFolderName;
            }

            var destinationFolder = DestinationFolder;

            failures.AddRange(m_RequestedFiles
                                .Where(f => destinationFolder.IsSame(f.DirectoryName))
                                .Select(f => $"File is already under destination folder: {f.FullName}")
                             );

            if (!failures.Any())
                return;

            m_JobResult = new JobResult(failures);
        }

        private async Task CopyFiles()
        {
            var destinationFolder = DestinationFolder;
            try
            {
                if (!Directory.Exists(destinationFolder))
                    Directory.CreateDirectory(destinationFolder);
            }
            catch (Exception ex)
            {
                m_JobResult = new JobResult($"Folder '{destinationFolder}' failed to create: \r\n {ex.Message}");
                return;
            }

            var f = 0;
            foreach (var file in m_RequestedFiles)
            {
                var destinationFile = Path.Combine(destinationFolder, file.Name);
                try
                {
                    File.Copy(file.FullName, destinationFile);
                }
                catch (Exception ex)
                {
                    m_JobResult = new JobResult($"Failed to copy '{file.Name}' to new '{destinationFolder}': \r\n {ex.Message}");
                    return;
                    // TODO: Clean Up?
                }

                f++; 
                OnProgress(Percentage(f), m_CurrentOperation);
            }            
        }

        private async Task VerifyCopiedFiles()
        {
            var destinationFolder = DestinationFolder;

            var notExist = new List<string>();
            var mismatchedSize = new List<string>();

            var f = 0;
            foreach (var file in m_RequestedFiles)
            {
                var destinationFile = Path.Combine(destinationFolder, file.Name);
                if (!File.Exists(destinationFile))
                    notExist.Add(file.Name);

                if (file.Length != new FileInfo(destinationFile).Length)
                    mismatchedSize.Add($"{file.Name} copied size was {new FileInfo(Path.Combine(destinationFolder, file.Name)).Length}. Expected{file.Length}");

                f++; OnProgress(Percentage(f), m_CurrentOperation);
            }

            if (notExist.Any())
            {
                m_JobResult = new JobResult("Files do not exist in destination", notExist);
                return;
            }           

            if (!mismatchedSize.Any())
                return;

            m_JobResult = new JobResult("Files mismatched sizes", mismatchedSize);
        }

        private async Task DeleteSourceFiles()
        {
            var f = 0;
            foreach (var file in m_RequestedFiles)
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception ex)
                {
                    m_JobResult = new JobResult($"Failed to delete original '{file.Name}': \r\n {ex.Message}");
                    return;
                    // TODO: Clean Up?
                }

                f++; 
                OnProgress(Percentage(f), m_CurrentOperation);
            }
        }

        private void OnProgress(int percentage, string message)
        {
            ProgressUpdate?.Invoke(this, new ProgressUpdateEventArgs(percentage, message));
        }

        private static Regex s_Regex = new Regex(":");

        public static DateTime GetDateTakenFromImage(FileInfo image)
        {
            //retrieves the datetime WITHOUT loading the whole image
            try
            {
                using (FileStream fs = new FileStream(image.FullName, FileMode.Open, FileAccess.Read))
                using (Image myImage = Image.FromStream(fs, false, false))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(36867);
                    string dateTaken = s_Regex.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.Parse(dateTaken);
                }
            }
            catch (ArgumentException ex)
            {
                Trace.WriteLine($"Exception getting Date Taken: {ex}");
                return image.CreationTime;
            }
        }

        private int Percentage(double f)
        {
            var p = f / m_RequestedFiles.Count;
            return (int)Math.Round(p * 100.0, 0, MidpointRounding.AwayFromZero);
        }
    }
}
