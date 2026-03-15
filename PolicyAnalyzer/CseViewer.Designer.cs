namespace PolicyAnalyzer
{
	// Token: 0x02000066 RID: 102
	public partial class CseViewer : global::System.Windows.Forms.Form
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00003381 File Offset: 0x00001581
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000104A4 File Offset: 0x0000E6A4
		private void InitializeComponent()
		{
			this.btnCopyList = new global::System.Windows.Forms.Button();
			this.btnOK = new global::System.Windows.Forms.Button();
			this.cmbGpoSetNames = new global::System.Windows.Forms.ComboBox();
			this.myRichTextBox1 = new global::PolicyAnalyzer.MyRichTextBox();
			base.SuspendLayout();
			this.btnCopyList.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnCopyList.Location = new global::System.Drawing.Point(947, 575);
			this.btnCopyList.Margin = new global::System.Windows.Forms.Padding(6);
			this.btnCopyList.Name = "btnCopyList";
			this.btnCopyList.Size = new global::System.Drawing.Size(150, 44);
			this.btnCopyList.TabIndex = 15;
			this.btnCopyList.Text = "Copy list";
			this.btnCopyList.UseVisualStyleBackColor = true;
			this.btnCopyList.Click += new global::System.EventHandler(this.btnCopyList_Click);
			this.btnOK.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.btnOK.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnOK.Location = new global::System.Drawing.Point(20, 575);
			this.btnOK.Margin = new global::System.Windows.Forms.Padding(6);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new global::System.Drawing.Size(150, 44);
			this.btnOK.TabIndex = 13;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.cmbGpoSetNames.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.cmbGpoSetNames.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbGpoSetNames.DropDownWidth = 736;
			this.cmbGpoSetNames.FormattingEnabled = true;
			this.cmbGpoSetNames.Location = new global::System.Drawing.Point(20, 20);
			this.cmbGpoSetNames.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cmbGpoSetNames.Name = "cmbGpoSetNames";
			this.cmbGpoSetNames.Size = new global::System.Drawing.Size(1077, 33);
			this.cmbGpoSetNames.TabIndex = 12;
			this.cmbGpoSetNames.SelectedIndexChanged += new global::System.EventHandler(this.cmbGpoSetNames_SelectedIndexChanged);
			this.myRichTextBox1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.myRichTextBox1.BackColor = global::System.Drawing.SystemColors.Window;
			this.myRichTextBox1.Location = new global::System.Drawing.Point(20, 64);
			this.myRichTextBox1.Margin = new global::System.Windows.Forms.Padding(6, 6, 6, 6);
			this.myRichTextBox1.Name = "myRichTextBox1";
			this.myRichTextBox1.Size = new global::System.Drawing.Size(1077, 494);
			this.myRichTextBox1.TabIndex = 16;
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(12f, 25f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnOK;
			base.ClientSize = new global::System.Drawing.Size(1117, 634);
			base.Controls.Add(this.myRichTextBox1);
			base.Controls.Add(this.btnCopyList);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.cmbGpoSetNames);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			this.MinimumSize = new global::System.Drawing.Size(790, 668);
			base.Name = "CseViewer";
			base.ShowIcon = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Show;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Client Side Extensions (CSEs)";
			base.ResumeLayout(false);
		}

		// Token: 0x04000731 RID: 1841
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000732 RID: 1842
		private global::System.Windows.Forms.Button btnCopyList;

		// Token: 0x04000733 RID: 1843
		private global::System.Windows.Forms.Button btnOK;

		// Token: 0x04000734 RID: 1844
		private global::System.Windows.Forms.ComboBox cmbGpoSetNames;

		// Token: 0x04000735 RID: 1845
		private global::PolicyAnalyzer.MyRichTextBox myRichTextBox1;
	}
}
