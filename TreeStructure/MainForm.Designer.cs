/*
 * 
###################################################
Created by SharpDevelop.
 * User: ashu
 * Date: 5/22/2008
 * Time: 11:25 PM
 * 
 * 
###################################################
To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace TreeStructure
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtRootDirectory = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.dlgFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.picProcessing = new System.Windows.Forms.PictureBox();
            this.bgWrkr = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProcessing)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.txtRootDirectory);
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select directory ";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(351, 18);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(25, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtRootDirectory
            // 
            this.txtRootDirectory.Location = new System.Drawing.Point(16, 20);
            this.txtRootDirectory.Name = "txtRootDirectory";
            this.txtRootDirectory.Size = new System.Drawing.Size(329, 21);
            this.txtRootDirectory.TabIndex = 0;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(319, 65);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(70, 22);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerateClick);
            // 
            // dlgSaveFile
            // 
            this.dlgSaveFile.Filter = "GIF File|*.gif|JPG File|*.jpg|PNG File|*.png|PS File|*.ps";
            // 
            // dlgFolder
            // 
            this.dlgFolder.Description = "Select a root folder for directory tree";
            this.dlgFolder.ShowNewFolderButton = false;
            // 
            // picProcessing
            // 
            this.picProcessing.Image = ((System.Drawing.Image)(resources.GetObject("picProcessing.Image")));
            this.picProcessing.Location = new System.Drawing.Point(9, 63);
            this.picProcessing.Name = "picProcessing";
            this.picProcessing.Size = new System.Drawing.Size(55, 52);
            this.picProcessing.TabIndex = 2;
            this.picProcessing.TabStop = false;
            this.picProcessing.Visible = false;
            // 
            // bgWrkr
            // 
            this.bgWrkr.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWrkr_DoWork);
            this.bgWrkr.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWrkr_Completed);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 119);
            this.Controls.Add(this.picProcessing);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tree Structure Generator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProcessing)).EndInit();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.TextBox txtRootDirectory;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SaveFileDialog dlgSaveFile;
        private System.Windows.Forms.FolderBrowserDialog dlgFolder;
        private System.Windows.Forms.PictureBox picProcessing;
        private System.ComponentModel.BackgroundWorker bgWrkr;
	}
}
