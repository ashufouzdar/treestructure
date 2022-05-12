#region Copyright © 2008 Ashu Fouzdar. All rights reserved.
/*
Copyright © 2008 Ashu Fouzdar. All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.
3. The name of the author may not be used to endorse or promote products
   derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE AUTHOR "AS IS" AND ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace TreeStructure
{
	/// <summary>
	/// MainForm of the application, Execution starts from this form.
	/// </summary>
	public partial class MainForm : Form
	{
		//Stream writer for writing DOT File
		private StreamWriter dotWriter=null;
        private string dotFilename = null;
        private string outputFilename = null;

        private string dotToolPath = null;
        //Variable to count the depth of recursion
        private int depth ;
        //Variable to increment node number for each element
        private int nodeCtr;

		public MainForm()
		{
            InitializeComponent();
		}
		
        //Button click event handler
		void BtnGenerateClick(object sender, EventArgs e)
		{
            if (txtRootDirectory.Text == string.Empty || !(Directory.Exists(txtRootDirectory.Text)))
            {
                MessageBox.Show("Root directory for directory tree is not specified or not exist", "Root directory invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dotToolPath = GetDotToolPath();
            if (dotToolPath==null || dotToolPath == string.Empty)
            {
                MessageBox.Show("GraphWiz toolkit is not installed in system or Dot tool is not in path", "Path missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dlgSaveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                outputFilename = dlgSaveFile.FileName;
            }
            
            this.picProcessing.Visible = true;
            this.bgWrkr.RunWorkerAsync();

            
		}
		
		void ProcessDirectory(DirectoryInfo dInfo,string ParentNode)
		{
            //Processing each subdirectory
            foreach (DirectoryInfo tempDir in dInfo.GetDirectories())
            {
                nodeCtr++;
                dotWriter.WriteLine("node{0} [shape=box,label=\"{1}\"]; ",nodeCtr,tempDir.Name);
                dotWriter.WriteLine("{0} -> node{1};", ParentNode, nodeCtr);

                //Recursive call: Increment depth and call recursively
                depth++;
                ProcessDirectory(tempDir,"node" + nodeCtr.ToString());
            }

            //Processing each files
            foreach(FileInfo fInfo in dInfo.GetFiles())
            {
                nodeCtr++;
                dotWriter.WriteLine("node{0} [shape=oval,label=\"{1}\"];", nodeCtr, fInfo.Name);
                dotWriter.WriteLine("{0} -> node{1};", ParentNode, nodeCtr);
            }

            depth--;
            
            if (depth == 0)
            {
                dotWriter.WriteLine(@"}");
                dotWriter.Close();

                if (outputFilename !=null && outputFilename !=string.Empty)
                {
                    string opFormat=Path.GetExtension(outputFilename);
                    opFormat=opFormat.Replace('.',' ').Trim();
                    Process pr = new Process();
                    pr.StartInfo.FileName = dotToolPath;
                    pr.StartInfo.Arguments = " -T" + opFormat + " " + ('"').ToString() + dotFilename + ('"').ToString() + " -o " + ('"').ToString() + outputFilename + ('"').ToString();
                    pr.StartInfo.CreateNoWindow = true;
                    pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pr.StartInfo.UseShellExecute = false;
                    pr.StartInfo.ErrorDialog=true;
                    pr.StartInfo.RedirectStandardOutput = true;

                    pr.Start();
                    pr.WaitForExit();
                    if (pr.ExitCode ==0)
                    {
                        if (MessageBox.Show("Tree structure written to : " + outputFilename + ", Do you want to open the generated file", "Succeed", MessageBoxButtons.YesNo, MessageBoxIcon.Information)==System.Windows.Forms.DialogResult.Yes)
                        {
                            Process.Start(outputFilename);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error Occurred : " + pr.StandardOutput.ReadToEnd() , "Failed", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }
                   
                }
                
            }
         }
	
        string GetDotToolPath() 
        {
            string dotToolPath=null;
            string ToolName = @"dot.exe";
            string pathEnvironmentVariable = Environment.GetEnvironmentVariable("PATH");
            string[] paths = pathEnvironmentVariable.Split(Path.PathSeparator);
            foreach (string path in paths)
            {
                string fullPathToClient = Path.Combine(path, ToolName);
                if (File.Exists(fullPathToClient))
                {
                    dotToolPath = fullPathToClient;
                    break;
                }
            }
            return dotToolPath;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (dlgFolder.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                txtRootDirectory.Text = dlgFolder.SelectedPath;
            }
        }

        private void bgWrkr_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            depth = 1;
            nodeCtr = 0;
            DirectoryInfo rootDir = new DirectoryInfo(txtRootDirectory.Text);
            dotFilename = Path.GetTempFileName();
            dotWriter = new StreamWriter(dotFilename, false);
            dotWriter.AutoFlush = true;
            dotWriter.WriteLine(@"digraph G {");
            dotWriter.WriteLine(@"node [shape=oval,fontsize=8];");
            dotWriter.WriteLine("node{0} [shape=diamond,style=filled,color=cyan,label=\"{1}\"];", nodeCtr, rootDir.Name);

            //Start traversing directory
            ProcessDirectory(rootDir, "node" + nodeCtr.ToString());
        }

        private void bgWrkr_Completed(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.picProcessing.Visible = false;
        }
    }
}
