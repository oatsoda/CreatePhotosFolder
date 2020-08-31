using CreatePhotosFolder.App.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CreatePhotosFolder.App.Job
{
    public class CreatePhotosFolderJob
    {
        private readonly JobSettings m_Settings;

        private JobResult m_JobResult;

        private string m_CurrentOperation;

        public event EventHandler<ProgressUpdateEventArgs> ProgressUpdate;
        
        public CreatePhotosFolderJob(JobSettings settings)
        {
            m_Settings = settings;
        }

        public async Task<JobResult> MoveFiles()
        {
            void Progress(object sender, ProgressUpdateEventArgs e)
            {
                m_Settings.UpdateAction(e);
            }

            ProgressUpdate += Progress;

            var result = await Task.Run(MoveFilesInternal);

            ProgressUpdate -= Progress;

            return result;
        }

        private JobResult MoveFilesInternal()
        {
            m_JobResult = new JobResult();
            
            m_CurrentOperation = "Validating files";
            OnProgress(0, m_CurrentOperation);

            PrepareFiles();

            if (!m_JobResult.Success)
                return m_JobResult;

            m_CurrentOperation = "Copying files to new location";
            OnProgress(0, m_CurrentOperation);

            CopyFiles();

            if (!m_JobResult.Success)
                return m_JobResult;

            m_CurrentOperation = "Verifying copied files";
            OnProgress(0, m_CurrentOperation);

            VerifyCopiedFiles();
            
            if (!m_JobResult.Success)
                return m_JobResult;

            m_CurrentOperation = "Deleting source files";
            OnProgress(0, m_CurrentOperation);

            DeleteSourceFiles();

            return m_JobResult;
        }

        private void PrepareFiles()
        {
            var failures = new List<string>();

            var minDate = DateTime.MaxValue;
            var maxDate = DateTime.MinValue;


            var c = 0;
            foreach (var file in m_Settings.RequestedFiles)
            {
                if (!file.Exists)
                {
                    failures.Add($"File does not exist: {file.FullName}");
                }
                else
                {
                    if (m_Settings.AddDatesToFolderName)
                    { 
                        var dateTaken = file.GetDateTakenFromImage();

                        if (file.CreationTime < minDate)
                            minDate = dateTaken;

                        if (file.CreationTime > maxDate)
                            maxDate = dateTaken;
                    }
                }

                c++; 
                OnProgress(Percentage(c), m_CurrentOperation);
            }

            if (m_Settings.AddDatesToFolderName)
            {
                var minDateStr = minDate.ToString("yyyy.MM.dd");
                var maxDateStr = maxDate.ToString("yyyy.MM.dd");
                if (maxDateStr == minDateStr)
                    maxDateStr = "";
                var newFolderName = $"{minDateStr} {m_Settings.AlbumFolderName} {maxDateStr}".TrimEnd();
                m_Settings.AlbumFolderName = newFolderName;
            }

            var destinationFolder = m_Settings.DestinationFolder;

            failures.AddRange(m_Settings.RequestedFiles
                                        .Where(f => destinationFolder.IsSameStringValue(f.DirectoryName))
                                        .Select(f => $"File is already under destination folder: {f.FullName}")
                             );

            if (!failures.Any())
                return;

            m_JobResult = new JobResult(failures);
        }

        private void CopyFiles()
        {
            var destinationFolder = m_Settings.DestinationFolder;
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
            foreach (var file in m_Settings.RequestedFiles)
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

        private void VerifyCopiedFiles()
        {
            var destinationFolder = m_Settings.DestinationFolder;

            var notExist = new List<string>();
            var mismatchedSize = new List<string>();

            var f = 0;
            foreach (var file in m_Settings.RequestedFiles)
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

        private void DeleteSourceFiles()
        {
            var f = 0;
            foreach (var file in m_Settings.RequestedFiles)
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
        
        private int Percentage(double f)
        {
            var p = f / m_Settings.RequestedFiles.Count;
            return (int)Math.Round(p * 100.0, 0, MidpointRounding.AwayFromZero);
        }
    }
}
