namespace PolicyAnalyzer
{
	// Token: 0x02000075 RID: 117
	public partial class GpoPicker3 : global::System.Windows.Forms.Form
	{
		// Token: 0x060001BE RID: 446 RVA: 0x000035D9 File Offset: 0x000017D9
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000128BC File Offset: 0x00010ABC
		private void InitializeComponent()
		{
			this.cmbGpoSetNames = new global::System.Windows.Forms.ComboBox();
			this.btnCopyList = new global::System.Windows.Forms.Button();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnOK = new global::System.Windows.Forms.Button();
			this.lblPolCount = new global::System.Windows.Forms.Label();
			this.chkSelectAll = new global::System.Windows.Forms.CheckBox();
			base.SuspendLayout();
			this.cmbGpoSetNames.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.cmbGpoSetNames.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbGpoSetNames.FormattingEnabled = true;
			this.cmbGpoSetNames.Location = new global::System.Drawing.Point(20, 20);
			this.cmbGpoSetNames.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cmbGpoSetNames.Name = "cmbGpoSetNames";
			this.cmbGpoSetNames.Size = new global::System.Drawing.Size(736, 33);
			this.cmbGpoSetNames.TabIndex = 0;
			this.cmbGpoSetNames.SelectedIndexChanged += new global::System.EventHandler(this.cmbGpoSetNames_SelectedIndexChanged);
			this.btnCopyList.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnCopyList.Location = new global::System.Drawing.Point(606, 570);
			this.btnCopyList.Margin = new global::System.Windows.Forms.Padding(6);
			this.btnCopyList.Name = "btnCopyList";
			this.btnCopyList.Size = new global::System.Drawing.Size(150, 44);
			this.btnCopyList.TabIndex = 8;
			this.btnCopyList.Text = "Copy list";
			this.btnCopyList.UseVisualStyleBackColor = true;
			this.btnCopyList.Click += new global::System.EventHandler(this.btnCopyList_Click);
			this.btnCancel.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new global::System.Drawing.Point(190, 570);
			this.btnCancel.Margin = new global::System.Windows.Forms.Padding(6);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(150, 44);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			this.btnOK.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.btnOK.Location = new global::System.Drawing.Point(28, 570);
			this.btnOK.Margin = new global::System.Windows.Forms.Padding(6);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new global::System.Drawing.Size(150, 44);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new global::System.EventHandler(this.btnOK_Click);
			this.lblPolCount.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.lblPolCount.AutoSize = true;
			this.lblPolCount.Location = new global::System.Drawing.Point(597, 75);
			this.lblPolCount.Margin = new global::System.Windows.Forms.Padding(6, 0, 6, 0);
			this.lblPolCount.Name = "lblPolCount";
			this.lblPolCount.Size = new global::System.Drawing.Size(166, 25);
			this.lblPolCount.TabIndex = 11;
			this.lblPolCount.Text = "GPO Count: xxx";
			this.lblPolCount.TextAlign = global::System.Drawing.ContentAlignment.TopRight;
			this.chkSelectAll.AutoSize = true;
			this.chkSelectAll.Location = new global::System.Drawing.Point(20, 75);
			this.chkSelectAll.Margin = new global::System.Windows.Forms.Padding(6);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new global::System.Drawing.Size(134, 29);
			this.chkSelectAll.TabIndex = 9;
			this.chkSelectAll.Text = "Select All";
			this.chkSelectAll.UseVisualStyleBackColor = true;
			this.chkSelectAll.CheckedChanged += new global::System.EventHandler(this.chkSelectAll_CheckedChanged);
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(12f, 25f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new global::System.Drawing.Size(776, 634);
			base.Controls.Add(this.lblPolCount);
			base.Controls.Add(this.chkSelectAll);
			base.Controls.Add(this.btnCopyList);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.cmbGpoSetNames);
			base.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			this.MinimumSize = new global::System.Drawing.Size(790, 668);
			base.Name = "GpoPicker3";
			base.ShowIcon = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Show;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "GPO Picker";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400076B RID: 1899
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400076C RID: 1900
		private global::System.Windows.Forms.ComboBox cmbGpoSetNames;

		// Token: 0x0400076D RID: 1901
		private global::System.Windows.Forms.Button btnCopyList;

		// Token: 0x0400076E RID: 1902
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x0400076F RID: 1903
		private global::System.Windows.Forms.Button btnOK;

		// Token: 0x04000770 RID: 1904
		private global::System.Windows.Forms.Label lblPolCount;

		// Token: 0x04000771 RID: 1905
		private global::System.Windows.Forms.CheckBox chkSelectAll;
	}
}
