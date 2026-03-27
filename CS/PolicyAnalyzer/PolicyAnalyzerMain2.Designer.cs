namespace PolicyAnalyzer
{
	// Token: 0x02000077 RID: 119
	public partial class PolicyAnalyzerMain2 : global::System.Windows.Forms.Form
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00003766 File Offset: 0x00001966
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00013DE0 File Offset: 0x00011FE0
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PolicyAnalyzer.PolicyAnalyzerMain2));
			this.columnHeader3 = new global::System.Windows.Forms.ColumnHeader();
			this.btnImport = new global::System.Windows.Forms.Button();
			this.btnPolicyRulesFolder = new global::System.Windows.Forms.Button();
			this.columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.listView1 = new global::System.Windows.Forms.ListView();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.label1 = new global::System.Windows.Forms.Label();
			this.toolTip1 = new global::System.Windows.Forms.ToolTip(this.components);
			this.lblNSelected = new global::System.Windows.Forms.Label();
			this.btnCompare3 = new global::System.Windows.Forms.Button();
			this.btn_DeleteSelected = new global::System.Windows.Forms.Button();
			this.chkSelectAll = new global::System.Windows.Forms.CheckBox();
			this.btnAdmxPath = new global::System.Windows.Forms.Button();
			this.label2 = new global::System.Windows.Forms.Label();
			this.btnResetPolicyRulesPath = new global::System.Windows.Forms.Button();
			this.btnResetPolicyDefsPath = new global::System.Windows.Forms.Button();
			this.btnCompareToEffectiveState = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.columnHeader3.Text = "Size";
			this.columnHeader3.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader3.Width = 78;
			this.btnImport.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnImport.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10.875f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnImport.Location = new global::System.Drawing.Point(894, 70);
			this.btnImport.Margin = new global::System.Windows.Forms.Padding(6);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new global::System.Drawing.Size(230, 84);
			this.btnImport.TabIndex = 1;
			this.btnImport.Text = "&Add ...";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new global::System.EventHandler(this.btnImport_Click);
			this.btnPolicyRulesFolder.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnPolicyRulesFolder.AutoEllipsis = true;
			this.btnPolicyRulesFolder.Location = new global::System.Drawing.Point(226, 756);
			this.btnPolicyRulesFolder.Margin = new global::System.Windows.Forms.Padding(6);
			this.btnPolicyRulesFolder.Name = "btnPolicyRulesFolder";
			this.btnPolicyRulesFolder.Size = new global::System.Drawing.Size(648, 44);
			this.btnPolicyRulesFolder.TabIndex = 5;
			this.btnPolicyRulesFolder.Text = "(Policy rules folder)";
			this.btnPolicyRulesFolder.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btnPolicyRulesFolder.UseVisualStyleBackColor = true;
			this.btnPolicyRulesFolder.Click += new global::System.EventHandler(this.btnPolicyRulesFolder_Click);
			this.columnHeader2.Text = "Date";
			this.columnHeader2.Width = 97;
			this.listView1.AllowColumnReorder = true;
			this.listView1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.listView1.CheckBoxes = true;
			this.listView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3 });
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new global::System.Drawing.Point(24, 70);
			this.listView1.Margin = new global::System.Windows.Forms.Padding(6);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new global::System.Drawing.Size(848, 671);
			this.listView1.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = global::System.Windows.Forms.View.Details;
			this.listView1.ColumnClick += new global::System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			this.listView1.ItemChecked += new global::System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
			this.listView1.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.listView1_KeyUp);
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 228;
			this.label1.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(22, 767);
			this.label1.Margin = new global::System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(195, 25);
			this.label1.TabIndex = 4;
			this.label1.Text = "&Policy Rule sets in:";
			this.lblNSelected.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblNSelected.Location = new global::System.Drawing.Point(708, 23);
			this.lblNSelected.Margin = new global::System.Windows.Forms.Padding(6, 0, 6, 0);
			this.lblNSelected.Name = "lblNSelected";
			this.lblNSelected.Size = new global::System.Drawing.Size(166, 25);
			this.lblNSelected.TabIndex = 13;
			this.lblNSelected.Text = "XXX selected";
			this.lblNSelected.TextAlign = global::System.Drawing.ContentAlignment.TopRight;
			this.btnCompare3.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnCompare3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10.875f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnCompare3.Location = new global::System.Drawing.Point(894, 166);
			this.btnCompare3.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCompare3.Name = "btnCompare3";
			this.btnCompare3.Size = new global::System.Drawing.Size(230, 205);
			this.btnCompare3.TabIndex = 2;
			this.btnCompare3.Text = "&View / Compare";
			this.btnCompare3.UseVisualStyleBackColor = true;
			this.btnCompare3.Click += new global::System.EventHandler(this.btnCompare3_Click);
			this.btn_DeleteSelected.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.btn_DeleteSelected.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10.875f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_DeleteSelected.Location = new global::System.Drawing.Point(894, 632);
			this.btn_DeleteSelected.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_DeleteSelected.Name = "btn_DeleteSelected";
			this.btn_DeleteSelected.Size = new global::System.Drawing.Size(230, 109);
			this.btn_DeleteSelected.TabIndex = 3;
			this.btn_DeleteSelected.Text = "&Delete selected";
			this.btn_DeleteSelected.UseVisualStyleBackColor = true;
			this.btn_DeleteSelected.Click += new global::System.EventHandler(this.btn_DeleteSelected_Click);
			this.chkSelectAll.AutoSize = true;
			this.chkSelectAll.Location = new global::System.Drawing.Point(28, 23);
			this.chkSelectAll.Margin = new global::System.Windows.Forms.Padding(6);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new global::System.Drawing.Size(134, 29);
			this.chkSelectAll.TabIndex = 10;
			this.chkSelectAll.Text = "&Select All";
			this.chkSelectAll.UseVisualStyleBackColor = true;
			this.chkSelectAll.CheckedChanged += new global::System.EventHandler(this.chkSelectAll_CheckedChanged);
			this.btnAdmxPath.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnAdmxPath.AutoEllipsis = true;
			this.btnAdmxPath.Location = new global::System.Drawing.Point(226, 812);
			this.btnAdmxPath.Margin = new global::System.Windows.Forms.Padding(6);
			this.btnAdmxPath.Name = "btnAdmxPath";
			this.btnAdmxPath.Size = new global::System.Drawing.Size(648, 44);
			this.btnAdmxPath.TabIndex = 8;
			this.btnAdmxPath.Text = "(Policy definitions folder)";
			this.btnAdmxPath.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btnAdmxPath.UseVisualStyleBackColor = true;
			this.btnAdmxPath.Click += new global::System.EventHandler(this.btnAdmxPath_Click);
			this.label2.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(22, 823);
			this.label2.Margin = new global::System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(206, 25);
			this.label2.TabIndex = 7;
			this.label2.Text = "Policy De&finitions in:";
			this.btnResetPolicyRulesPath.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnResetPolicyRulesPath.Location = new global::System.Drawing.Point(894, 756);
			this.btnResetPolicyRulesPath.Name = "btnResetPolicyRulesPath";
			this.btnResetPolicyRulesPath.Size = new global::System.Drawing.Size(141, 44);
			this.btnResetPolicyRulesPath.TabIndex = 6;
			this.btnResetPolicyRulesPath.Text = "Reset";
			this.btnResetPolicyRulesPath.UseVisualStyleBackColor = true;
			this.btnResetPolicyRulesPath.Click += new global::System.EventHandler(this.btnResetPolicyRulesPath_Click);
			this.btnResetPolicyDefsPath.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnResetPolicyDefsPath.Location = new global::System.Drawing.Point(894, 812);
			this.btnResetPolicyDefsPath.Name = "btnResetPolicyDefsPath";
			this.btnResetPolicyDefsPath.Size = new global::System.Drawing.Size(141, 44);
			this.btnResetPolicyDefsPath.TabIndex = 9;
			this.btnResetPolicyDefsPath.Text = "Reset";
			this.btnResetPolicyDefsPath.UseVisualStyleBackColor = true;
			this.btnResetPolicyDefsPath.Click += new global::System.EventHandler(this.btnResetPolicyDefsPath_Click);
			this.btnCompareToEffectiveState.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnCompareToEffectiveState.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10.875f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnCompareToEffectiveState.Location = new global::System.Drawing.Point(894, 379);
			this.btnCompareToEffectiveState.Name = "btnCompareToEffectiveState";
			this.btnCompareToEffectiveState.Size = new global::System.Drawing.Size(230, 245);
			this.btnCompareToEffectiveState.TabIndex = 14;
			this.btnCompareToEffectiveState.Text = "Compare to Effective State";
			this.btnCompareToEffectiveState.UseVisualStyleBackColor = true;
			this.btnCompareToEffectiveState.Click += new global::System.EventHandler(this.btnCompareToEffectiveState_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(12f, 25f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(1142, 876);
			base.Controls.Add(this.btnCompareToEffectiveState);
			base.Controls.Add(this.btnResetPolicyDefsPath);
			base.Controls.Add(this.btnResetPolicyRulesPath);
			base.Controls.Add(this.btnAdmxPath);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.chkSelectAll);
			base.Controls.Add(this.btn_DeleteSelected);
			base.Controls.Add(this.btnCompare3);
			base.Controls.Add(this.lblNSelected);
			base.Controls.Add(this.btnImport);
			base.Controls.Add(this.btnPolicyRulesFolder);
			base.Controls.Add(this.listView1);
			base.Controls.Add(this.label1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(6);
			this.MinimumSize = new global::System.Drawing.Size(1168, 947);
			base.Name = "PolicyAnalyzerMain2";
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Policy Analyzer";
			base.Load += new global::System.EventHandler(this.PolicyAnalyzerMain2_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400077C RID: 1916
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400077D RID: 1917
		private global::System.Windows.Forms.ColumnHeader columnHeader3;

		// Token: 0x0400077E RID: 1918
		private global::System.Windows.Forms.Button btnImport;

		// Token: 0x0400077F RID: 1919
		private global::System.Windows.Forms.Button btnPolicyRulesFolder;

		// Token: 0x04000780 RID: 1920
		private global::System.Windows.Forms.ColumnHeader columnHeader2;

		// Token: 0x04000781 RID: 1921
		private global::System.Windows.Forms.ListView listView1;

		// Token: 0x04000782 RID: 1922
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x04000783 RID: 1923
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000784 RID: 1924
		private global::System.Windows.Forms.ToolTip toolTip1;

		// Token: 0x04000785 RID: 1925
		private global::System.Windows.Forms.Label lblNSelected;

		// Token: 0x04000786 RID: 1926
		private global::System.Windows.Forms.Button btnCompare3;

		// Token: 0x04000787 RID: 1927
		private global::System.Windows.Forms.Button btn_DeleteSelected;

		// Token: 0x04000788 RID: 1928
		private global::System.Windows.Forms.CheckBox chkSelectAll;

		// Token: 0x04000789 RID: 1929
		private global::System.Windows.Forms.Button btnAdmxPath;

		// Token: 0x0400078A RID: 1930
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400078B RID: 1931
		private global::System.Windows.Forms.Button btnResetPolicyRulesPath;

		// Token: 0x0400078C RID: 1932
		private global::System.Windows.Forms.Button btnResetPolicyDefsPath;

		// Token: 0x0400078D RID: 1933
		private global::System.Windows.Forms.Button btnCompareToEffectiveState;
	}
}
