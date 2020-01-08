using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace CreatePhotosFolder
{
    public partial class CreatePhotosFolderForm : Form
    {
        private readonly CreatePhotosFolderJob m_Job;

        public CreatePhotosFolderForm(string[] args)
        {            
            InitializeComponent();

            m_Job = new CreatePhotosFolderJob(args);
            m_Job.ProgressUpdate += M_Job_ProgressUpdate;

            Height -= bottomPanel.Height;
        }

        protected override void OnLoad(EventArgs e)
        {
            lblFileCount.Text = m_Job.TotalFiles.ToString();
            lblImageCount.Text = m_Job.TotalImageFiles.ToString();

            var parentFolders = new List<ParentFolder>
            {
                new ParentFolder("Pictures", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures))
            };

            var custom = ConfigurationManager.AppSettings["CustomPath"];
            if (!string.IsNullOrWhiteSpace(custom))
                parentFolders.Add(new ParentFolder(custom, custom));

            cbParent.DataSource = parentFolders;
            
            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            cbFolder.Focus();
        }

        #region Control Updates

        private void CbParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Job.ParentFolder = (ParentFolder)cbParent.SelectedItem;
        }

        private void CbFolder_TextUpdate(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrWhiteSpace(cbFolder.Text);
            m_Job.AlbumFolderName = cbFolder.Text;
        }

        private void CbDates_CheckedChanged(object sender, EventArgs e)
        {
            m_Job.AddDatesToFolderName = cbDates.Checked;
        }

        #endregion

        #region Job Updates

        private void M_Job_ProgressUpdate(object sender, ProgressUpdateEventArgs e)
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
            Height += bottomPanel.Height;
            var result = await m_Job.MoveFiles();
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
