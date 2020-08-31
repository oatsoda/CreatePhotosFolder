using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CreatePhotosFolder.App.Extensions;
using CreatePhotosFolder.App.Job;
using CreatePhotosFolder.App.Settings;

namespace CreatePhotosFolder.App
{
    public partial class CreatePhotosFolderForm : Form
    {
        private readonly JobSettings m_JobSettings;

        public CreatePhotosFolderForm(IEnumerable<string> args)
        {            
            InitializeComponent();

            m_JobSettings = new JobSettings(args, JobProgressUpdate);
            
            Height -= bottomPanel.Height;
        }

        protected override void OnLoad(EventArgs e)
        {
            lblFileCount.Text = m_JobSettings.TotalFiles.ToString();
            lblImageCount.Text = m_JobSettings.TotalImageFiles.ToString();

            cbDates.Checked = UserSettings.Settings.PrependDates;
            
            var parentFolders = RootFolders.LoadPhotoRootFolders();
            cbParent.DataSource = parentFolders;
            var lastParentFolder = parentFolders.FirstOrDefault(f => string.Equals(f.Name, UserSettings.Settings.LastRootFolder, StringComparison.InvariantCultureIgnoreCase));
            if (lastParentFolder != null)
                cbParent.SelectedItem = lastParentFolder;

            cbFolder.Text = UserSettings.Settings.LastAlbumFolder;
            cbFolder.SelectAll();

            SetJobSettings();

            base.OnLoad(e);
        }
        
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            cbFolder.Focus();
        }

        private void SetJobSettings()
        {
            m_JobSettings.AddDatesToFolderName = cbDates.Checked;
            m_JobSettings.RootFolder = (RootFolder)cbParent.SelectedItem;
            m_JobSettings.AlbumFolderName = cbFolder.Text;
            btnOK.Enabled = m_JobSettings.SettingsValid;
        }

        private void SaveUserSettings()
        {
            UserSettings.Settings.PrependDates = cbDates.Checked;
            UserSettings.Settings.LastRootFolder = cbParent.Text;
            UserSettings.Settings.LastAlbumFolder = cbFolder.Text;
            UserSettings.Settings.Save();
        }

        #region Control Updates

        private void CbParent_SelectedIndexChanged(object sender, EventArgs e) => SetJobSettings();
        private void CbFolder_TextUpdate(object sender, EventArgs e) => SetJobSettings();
        private void CbDates_CheckedChanged(object sender, EventArgs e) => SetJobSettings();

        #endregion

        #region Job Updates

        private void JobProgressUpdate(ProgressUpdateEventArgs e)
        {
            this.InvokeIfRequired(() =>
            {
                progressBar.Value = e.Percentage;
                lblProgress.Text = e.Message;
            });
        }

        #endregion

        #region Button Handlers

        private async void BtnOK_Click(object sender, EventArgs e)
        {
            mainPanel.Enabled = false;
            bottomPanel.Visible = bottomPanel.Enabled = true;

            SaveUserSettings();

            Height += bottomPanel.Height;

            var job = new CreatePhotosFolderJob(m_JobSettings);
            var result = await job.MoveFiles();

            lblProgress.Text = result.Success ? "Files moved successfully" : $"Failed: {result.FailureReason}";
            btnClose.Enabled = true;

            if (!result.Success)
                MessageBox.Show(this, result.FailureReason, "Failed to move files", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
