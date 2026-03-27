using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PolicyAnalyzer
{
	// Token: 0x02000091 RID: 145
	public partial class ProcessRunning : Form
	{
		// Token: 0x06000264 RID: 612 RVA: 0x00003A55 File Offset: 0x00001C55
		public ProcessRunning(string sTitle, string sCmdLine, int progMinimum = 0, int progMaximum = 100)
		{
			this.InitializeComponent();
			this.Text = sTitle;
			this.lblCmdLine.Text = sCmdLine;
			this.progressBar1.Minimum = progMinimum;
			this.progressBar1.Maximum = progMaximum;
			ProcessRunning.st_CurrentProgressDialog = this;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00003A95 File Offset: 0x00001C95
		private void ProcessRunning_Load(object sender, EventArgs e)
		{
			base.CenterToParent();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00019314 File Offset: 0x00017514
		public static void Progress(int minimum, int maximum, int current)
		{
			if (ProcessRunning.st_CurrentProgressDialog != null)
			{
				ProcessRunning.st_CurrentProgressDialog.progressBar1.Minimum = minimum;
				ProcessRunning.st_CurrentProgressDialog.progressBar1.Maximum = maximum;
				ProcessRunning.st_CurrentProgressDialog.progressBar1.Value = current;
				ProcessRunning.st_CurrentProgressDialog.progressBar1.Update();
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00019368 File Offset: 0x00017568
		public static string SetProgressText(string sCmdLine)
		{
			string text = string.Empty;
			if (ProcessRunning.st_CurrentProgressDialog != null)
			{
				text = ProcessRunning.st_CurrentProgressDialog.lblCmdLine.Text;
				ProcessRunning.st_CurrentProgressDialog.lblCmdLine.Text = sCmdLine;
				ProcessRunning.st_CurrentProgressDialog.lblCmdLine.Update();
			}
			return text;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00003A9D File Offset: 0x00001C9D
		private void ProcessRunning_FormClosed(object sender, FormClosedEventArgs e)
		{
			ProcessRunning.st_CurrentProgressDialog = null;
		}

		// Token: 0x04000803 RID: 2051
		private static ProcessRunning st_CurrentProgressDialog;
	}
}
