using CreatePhotosFolder.App.Settings;

namespace CreatePhotosFolder.App
{
    partial class CreatePhotosFolderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatePhotosFolderForm));
            this.cbParent = new System.Windows.Forms.ComboBox();
            this.parentFolderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cbFolder = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFileCount = new System.Windows.Forms.Label();
            this.lblImageCount = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.cbDates = new System.Windows.Forms.CheckBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.parentFolderBindingSource)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbParent
            // 
            this.cbParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbParent.DataSource = this.parentFolderBindingSource;
            this.cbParent.DisplayMember = "Name";
            this.cbParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParent.FormattingEnabled = true;
            this.cbParent.Location = new System.Drawing.Point(98, 71);
            this.cbParent.Name = "cbParent";
            this.cbParent.Size = new System.Drawing.Size(419, 21);
            this.cbParent.TabIndex = 0;
            this.cbParent.SelectedIndexChanged += new System.EventHandler(this.CbParent_SelectedIndexChanged);
            // 
            // parentFolderBindingSource
            // 
            this.parentFolderBindingSource.DataSource = typeof(RootFolder);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Photos Folder:";
            // 
            // cbFolder
            // 
            this.cbFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFolder.DataSource = this.parentFolderBindingSource;
            this.cbFolder.DisplayMember = "Name";
            this.cbFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbFolder.FormattingEnabled = true;
            this.cbFolder.Location = new System.Drawing.Point(98, 15);
            this.cbFolder.Name = "cbFolder";
            this.cbFolder.Size = new System.Drawing.Size(419, 21);
            this.cbFolder.TabIndex = 1;
            this.cbFolder.TextUpdate += new System.EventHandler(this.CbFolder_TextUpdate);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Album Name:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(441, 132);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(359, 132);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 48);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(506, 23);
            this.progressBar.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Files Selected:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Of Which Images:";
            // 
            // lblFileCount
            // 
            this.lblFileCount.AutoSize = true;
            this.lblFileCount.Location = new System.Drawing.Point(129, 110);
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(13, 13);
            this.lblFileCount.TabIndex = 9;
            this.lblFileCount.Text = "0";
            // 
            // lblImageCount
            // 
            this.lblImageCount.AutoSize = true;
            this.lblImageCount.Location = new System.Drawing.Point(129, 135);
            this.lblImageCount.Name = "lblImageCount";
            this.lblImageCount.Size = new System.Drawing.Size(13, 13);
            this.lblImageCount.TabIndex = 10;
            this.lblImageCount.Text = "0";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.cbDates);
            this.mainPanel.Controls.Add(this.lblImageCount);
            this.mainPanel.Controls.Add(this.label3);
            this.mainPanel.Controls.Add(this.lblFileCount);
            this.mainPanel.Controls.Add(this.cbParent);
            this.mainPanel.Controls.Add(this.label4);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Controls.Add(this.cbFolder);
            this.mainPanel.Controls.Add(this.btnOK);
            this.mainPanel.Controls.Add(this.label2);
            this.mainPanel.Controls.Add(this.btnCancel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(529, 167);
            this.mainPanel.TabIndex = 11;
            // 
            // cbDates
            // 
            this.cbDates.AutoSize = true;
            this.cbDates.Location = new System.Drawing.Point(371, 42);
            this.cbDates.Name = "cbDates";
            this.cbDates.Size = new System.Drawing.Size(146, 17);
            this.cbDates.TabIndex = 12;
            this.cbDates.Text = "Add dates to album name";
            this.cbDates.UseVisualStyleBackColor = true;
            this.cbDates.CheckedChanged += new System.EventHandler(this.CbDates_CheckedChanged);
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.Location = new System.Drawing.Point(12, 11);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(505, 24);
            this.lblProgress.TabIndex = 11;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.btnClose);
            this.bottomPanel.Controls.Add(this.lblProgress);
            this.bottomPanel.Controls.Add(this.progressBar);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Enabled = false;
            this.bottomPanel.Location = new System.Drawing.Point(0, 166);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(529, 122);
            this.bottomPanel.TabIndex = 12;
            this.bottomPanel.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(229, 85);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(76, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // CreatePhotosFolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 288);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreatePhotosFolderForm";
            this.Text = "Move to Photo Album";
            ((System.ComponentModel.ISupportInitialize)(this.parentFolderBindingSource)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbParent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.Label lblImageCount;
        private System.Windows.Forms.BindingSource parentFolderBindingSource;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.CheckBox cbDates;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button btnClose;
    }
}

