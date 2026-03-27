namespace PolicyAnalyzer
{
	// Token: 0x02000073 RID: 115
	public partial class FindDlg : global::System.Windows.Forms.Form
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000353E File Offset: 0x0000173E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00011EC4 File Offset: 0x000100C4
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.txtFindText = new global::System.Windows.Forms.TextBox();
			this.btnFindNext = new global::System.Windows.Forms.Button();
			this.btnCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(56, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Fi&nd what:";
			this.txtFindText.Location = new global::System.Drawing.Point(76, 10);
			this.txtFindText.Name = "txtFindText";
			this.txtFindText.Size = new global::System.Drawing.Size(189, 20);
			this.txtFindText.TabIndex = 1;
			this.txtFindText.TextChanged += new global::System.EventHandler(this.txtFindText_TextChanged);
			this.btnFindNext.Enabled = false;
			this.btnFindNext.Location = new global::System.Drawing.Point(271, 9);
			this.btnFindNext.Name = "btnFindNext";
			this.btnFindNext.Size = new global::System.Drawing.Size(75, 23);
			this.btnFindNext.TabIndex = 2;
			this.btnFindNext.Text = "&Find Next";
			this.btnFindNext.UseVisualStyleBackColor = true;
			this.btnFindNext.Click += new global::System.EventHandler(this.btnFindNext_Click);
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new global::System.Drawing.Point(271, 39);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			base.AcceptButton = this.btnFindNext;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new global::System.Drawing.Size(358, 104);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnFindNext);
			base.Controls.Add(this.txtFindText);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FindDlg";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Find";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000754 RID: 1876
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000755 RID: 1877
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000756 RID: 1878
		private global::System.Windows.Forms.TextBox txtFindText;

		// Token: 0x04000757 RID: 1879
		private global::System.Windows.Forms.Button btnFindNext;

		// Token: 0x04000758 RID: 1880
		private global::System.Windows.Forms.Button btnCancel;
	}
}
