namespace PolicyAnalyzer
{
	// Token: 0x02000091 RID: 145
	public partial class ProcessRunning : global::System.Windows.Forms.Form
	{
		// Token: 0x06000269 RID: 617 RVA: 0x00003AA5 File Offset: 0x00001CA5
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000193B4 File Offset: 0x000175B4
		private void InitializeComponent()
		{
			this.lblCmdLine = new global::System.Windows.Forms.Label();
			this.progressBar1 = new global::System.Windows.Forms.ProgressBar();
			base.SuspendLayout();
			this.lblCmdLine.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.lblCmdLine.Location = new global::System.Drawing.Point(0, 0);
			this.lblCmdLine.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCmdLine.Name = "lblCmdLine";
			this.lblCmdLine.Size = new global::System.Drawing.Size(825, 114);
			this.lblCmdLine.TabIndex = 2;
			this.lblCmdLine.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.lblCmdLine.UseWaitCursor = true;
			this.progressBar1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.progressBar1.Location = new global::System.Drawing.Point(0, 131);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new global::System.Drawing.Size(825, 23);
			this.progressBar1.TabIndex = 3;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(825, 154);
			base.ControlBox = false;
			base.Controls.Add(this.progressBar1);
			base.Controls.Add(this.lblCmdLine);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Margin = new global::System.Windows.Forms.Padding(4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ProcessRunning";
			base.ShowInTaskbar = false;
			this.Text = "Process Running";
			base.UseWaitCursor = true;
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.ProcessRunning_FormClosed);
			base.Load += new global::System.EventHandler(this.ProcessRunning_Load);
			base.ResumeLayout(false);
		}

		// Token: 0x04000804 RID: 2052
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000805 RID: 2053
		private global::System.Windows.Forms.Label lblCmdLine;

		// Token: 0x04000806 RID: 2054
		private global::System.Windows.Forms.ProgressBar progressBar1;
	}
}
