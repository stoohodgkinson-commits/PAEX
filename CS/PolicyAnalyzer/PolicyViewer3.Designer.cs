namespace PolicyAnalyzer
{
	// Token: 0x02000079 RID: 121
	public partial class PolicyViewer3 : global::System.Windows.Forms.Form
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00003924 File Offset: 0x00001B24
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000174E8 File Offset: 0x000156E8
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PolicyAnalyzer.PolicyViewer3));
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new global::System.Windows.Forms.DataGridViewCellStyle();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.clipboardToolStripDropDown = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.selectAllToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripDropDownButton4 = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.showOnlyDifferencesToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.showOnlyConflictToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.showDetailsPaneToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.gPOFilterToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripDropDownButton3 = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.findToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.findAgainToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripDropDownButton2 = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.ExportToExcelToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.exportAllDataToExcelToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.exportAllDataToCSVToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripDropDownButton1 = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.fontToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.showGPONamesAndFilesInDetailsPaneToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.showExplanationTextForSettingsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.dataGridView1 = new global::System.Windows.Forms.DataGridView();
			this.colPolicyType = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colPolicyGroupOrRegKey = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colPolicySetting = new global::System.Windows.Forms.DataGridViewTextBoxColumn();
			this.splitContainer1 = new global::System.Windows.Forms.SplitContainer();
			this.clientSideExtensionsCSEsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.rtDetails = new global::PolicyAnalyzer.MyRichTextBox();
			this.showGPONamesInDetailsPaneToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.clipboardToolStripDropDown, this.toolStripDropDownButton4, this.toolStripDropDownButton3, this.toolStripDropDownButton2, this.toolStripDropDownButton1 });
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.ShowItemToolTips = false;
			this.toolStrip1.Size = new global::System.Drawing.Size(1317, 27);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			this.clipboardToolStripDropDown.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.clipboardToolStripDropDown.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.selectAllToolStripMenuItem, this.copyToolStripMenuItem });
			this.clipboardToolStripDropDown.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("clipboardToolStripDropDown.Image");
			this.clipboardToolStripDropDown.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.clipboardToolStripDropDown.Name = "clipboardToolStripDropDown";
			this.clipboardToolStripDropDown.Size = new global::System.Drawing.Size(89, 24);
			this.clipboardToolStripDropDown.Text = "&Clipboard";
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)131137;
			this.selectAllToolStripMenuItem.Size = new global::System.Drawing.Size(198, 26);
			this.selectAllToolStripMenuItem.Text = "Select All";
			this.selectAllToolStripMenuItem.Click += new global::System.EventHandler(this.selectAllToolStripMenuItem_Click);
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)131139;
			this.copyToolStripMenuItem.Size = new global::System.Drawing.Size(198, 26);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new global::System.EventHandler(this.copyToolStripMenuItem_Click);
			this.toolStripDropDownButton4.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton4.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.toolStripSeparator1, this.showOnlyDifferencesToolStripMenuItem, this.showOnlyConflictToolStripMenuItem, this.toolStripSeparator2, this.showDetailsPaneToolStripMenuItem1, this.gPOFilterToolStripMenuItem, this.clientSideExtensionsCSEsToolStripMenuItem });
			this.toolStripDropDownButton4.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripDropDownButton4.Image");
			this.toolStripDropDownButton4.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
			this.toolStripDropDownButton4.Size = new global::System.Drawing.Size(55, 24);
			this.toolStripDropDownButton4.Text = "&View";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(228, 6);
			this.showOnlyDifferencesToolStripMenuItem.Name = "showOnlyDifferencesToolStripMenuItem";
			this.showOnlyDifferencesToolStripMenuItem.Size = new global::System.Drawing.Size(231, 26);
			this.showOnlyDifferencesToolStripMenuItem.Text = "Show only Differences";
			this.showOnlyDifferencesToolStripMenuItem.Click += new global::System.EventHandler(this.showOnlyDifferencesToolStripMenuItem_Click);
			this.showOnlyConflictToolStripMenuItem.Name = "showOnlyConflictToolStripMenuItem";
			this.showOnlyConflictToolStripMenuItem.Size = new global::System.Drawing.Size(231, 26);
			this.showOnlyConflictToolStripMenuItem.Text = "Show only Conflicts";
			this.showOnlyConflictToolStripMenuItem.Click += new global::System.EventHandler(this.showOnlyConflictToolStripMenuItem_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(228, 6);
			this.showDetailsPaneToolStripMenuItem1.Name = "showDetailsPaneToolStripMenuItem1";
			this.showDetailsPaneToolStripMenuItem1.Size = new global::System.Drawing.Size(231, 26);
			this.showDetailsPaneToolStripMenuItem1.Text = "Show Details Pane";
			this.showDetailsPaneToolStripMenuItem1.Click += new global::System.EventHandler(this.showDetailsPaneToolStripMenuItem1_Click);
			this.gPOFilterToolStripMenuItem.Name = "gPOFilterToolStripMenuItem";
			this.gPOFilterToolStripMenuItem.Size = new global::System.Drawing.Size(231, 26);
			this.gPOFilterToolStripMenuItem.Text = "GPO filter...";
			this.gPOFilterToolStripMenuItem.Click += new global::System.EventHandler(this.gPOFilterToolStripMenuItem_Click);
			this.toolStripDropDownButton3.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButton3.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.findToolStripMenuItem, this.findAgainToolStripMenuItem });
			this.toolStripDropDownButton3.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripDropDownButton3.Image");
			this.toolStripDropDownButton3.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
			this.toolStripDropDownButton3.Size = new global::System.Drawing.Size(34, 24);
			this.toolStripDropDownButton3.Text = "toolStripDropDownButton3";
			this.toolStripDropDownButton3.ToolTipText = "Find options";
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			this.findToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)131142;
			this.findToolStripMenuItem.Size = new global::System.Drawing.Size(186, 26);
			this.findToolStripMenuItem.Text = "Find...";
			this.findToolStripMenuItem.Click += new global::System.EventHandler(this.findToolStripMenuItem_Click);
			this.findAgainToolStripMenuItem.Name = "findAgainToolStripMenuItem";
			this.findAgainToolStripMenuItem.ShortcutKeys = global::System.Windows.Forms.Keys.F3;
			this.findAgainToolStripMenuItem.Size = new global::System.Drawing.Size(186, 26);
			this.findAgainToolStripMenuItem.Text = "Find again...";
			this.findAgainToolStripMenuItem.Click += new global::System.EventHandler(this.findAgainToolStripMenuItem_Click);
			this.toolStripDropDownButton2.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton2.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.ExportToExcelToolStripMenuItem, this.exportAllDataToExcelToolStripMenuItem, this.exportAllDataToCSVToolStripMenuItem });
			this.toolStripDropDownButton2.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripDropDownButton2.Image");
			this.toolStripDropDownButton2.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
			this.toolStripDropDownButton2.Size = new global::System.Drawing.Size(66, 24);
			this.toolStripDropDownButton2.Text = "&Export";
			this.ExportToExcelToolStripMenuItem.Name = "ExportToExcelToolStripMenuItem";
			this.ExportToExcelToolStripMenuItem.Size = new global::System.Drawing.Size(237, 26);
			this.ExportToExcelToolStripMenuItem.Text = "Export table to Excel";
			this.ExportToExcelToolStripMenuItem.Click += new global::System.EventHandler(this.ExportToExcelToolStripMenuItem_Click);
			this.exportAllDataToExcelToolStripMenuItem.Name = "exportAllDataToExcelToolStripMenuItem";
			this.exportAllDataToExcelToolStripMenuItem.Size = new global::System.Drawing.Size(237, 26);
			this.exportAllDataToExcelToolStripMenuItem.Text = "Export all data to Excel";
			this.exportAllDataToExcelToolStripMenuItem.Click += new global::System.EventHandler(this.exportAllDataToExcelToolStripMenuItem_Click);
			this.exportAllDataToCSVToolStripMenuItem.Name = "exportAllDataToCSVToolSTripMenuItem";
			this.exportAllDataToCSVToolStripMenuItem.Size = new global::System.Drawing.Size(237, 26);
			this.exportAllDataToCSVToolStripMenuItem.Text = "Export all data to CSV";
			this.exportAllDataToCSVToolStripMenuItem.Click += new global::System.EventHandler(this.exportAllDataToCSVToolStripMenuItem_Click);
			this.toolStripDropDownButton1.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.fontToolStripMenuItem, this.showGPONamesInDetailsPaneToolStripMenuItem, this.showGPONamesAndFilesInDetailsPaneToolStripMenuItem, this.showExplanationTextForSettingsToolStripMenuItem });
			this.toolStripDropDownButton1.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripDropDownButton1.Image");
			this.toolStripDropDownButton1.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new global::System.Drawing.Size(75, 24);
			this.toolStripDropDownButton1.Text = "Options";
			this.fontToolStripMenuItem.Enabled = false;
			this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
			this.fontToolStripMenuItem.Size = new global::System.Drawing.Size(363, 26);
			this.fontToolStripMenuItem.Text = "Font...";
			this.fontToolStripMenuItem.Visible = false;
			this.showGPONamesAndFilesInDetailsPaneToolStripMenuItem.Name = "showGPONamesAndFilesInDetailsPaneToolStripMenuItem";
			this.showGPONamesAndFilesInDetailsPaneToolStripMenuItem.Size = new global::System.Drawing.Size(363, 26);
			this.showGPONamesAndFilesInDetailsPaneToolStripMenuItem.Text = "Show GPO names and files in Details pane";
			this.showGPONamesAndFilesInDetailsPaneToolStripMenuItem.Click += new global::System.EventHandler(this.showGPONamesAndFilesInDetailsPaneToolStripMenuItem_Click);
			this.showExplanationTextForSettingsToolStripMenuItem.Checked = true;
			this.showExplanationTextForSettingsToolStripMenuItem.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.showExplanationTextForSettingsToolStripMenuItem.Name = "showExplanationTextForSettingsToolStripMenuItem";
			this.showExplanationTextForSettingsToolStripMenuItem.Size = new global::System.Drawing.Size(363, 26);
			this.showExplanationTextForSettingsToolStripMenuItem.Text = "Show explanation text for settings";
			this.showExplanationTextForSettingsToolStripMenuItem.Click += new global::System.EventHandler(this.showExplanationTextForSettingsToolStripMenuItem_Click);
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.BackgroundColor = global::System.Drawing.SystemColors.Window;
			dataGridViewCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 7.8f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView1.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new global::System.Windows.Forms.DataGridViewColumn[] { this.colPolicyType, this.colPolicyGroupOrRegKey, this.colPolicySetting });
			dataGridViewCellStyle2.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = global::System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8f);
			dataGridViewCellStyle2.ForeColor = global::System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new global::System.Drawing.Point(0, 27);
			this.dataGridView1.Margin = new global::System.Windows.Forms.Padding(4);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 7.8f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.Height = 16;
			this.dataGridView1.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new global::System.Drawing.Size(1317, 461);
			this.dataGridView1.TabIndex = 3;
			this.dataGridView1.SelectionChanged += new global::System.EventHandler(this.dataGridView1_SelectionChanged);
			this.colPolicyType.HeaderText = "Policy Type";
			this.colPolicyType.Name = "colPolicyType";
			this.colPolicyType.ReadOnly = true;
			this.colPolicyType.Width = 110;
			this.colPolicyGroupOrRegKey.HeaderText = "Policy Group or Registry Key";
			this.colPolicyGroupOrRegKey.Name = "colPolicyGroupOrRegKey";
			this.colPolicyGroupOrRegKey.ReadOnly = true;
			this.colPolicyGroupOrRegKey.Width = 400;
			this.colPolicySetting.HeaderText = "Policy Setting";
			this.colPolicySetting.Name = "colPolicySetting";
			this.colPolicySetting.ReadOnly = true;
			this.colPolicySetting.Width = 180;
			this.splitContainer1.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new global::System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = global::System.Windows.Forms.Orientation.Horizontal;
			this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
			this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
			this.splitContainer1.Panel2.Controls.Add(this.rtDetails);
			this.splitContainer1.Size = new global::System.Drawing.Size(1321, 834);
			this.splitContainer1.SplitterDistance = 492;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 4;
			this.rtDetails.BackColor = global::System.Drawing.SystemColors.Window;
			this.rtDetails.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.rtDetails.Font = new global::System.Drawing.Font("Lucida Console", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.rtDetails.Location = new global::System.Drawing.Point(0, 0);
			this.rtDetails.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.rtDetails.Name = "rtDetails";
			this.rtDetails.Size = new global::System.Drawing.Size(1317, 332);
			this.rtDetails.TabIndex = 0;
			this.showGPONamesInDetailsPaneToolStripMenuItem.Name = "showGPONamesInDetailsPaneToolStripMenuItem";
			this.showGPONamesInDetailsPaneToolStripMenuItem.Size = new global::System.Drawing.Size(363, 26);
			this.showGPONamesInDetailsPaneToolStripMenuItem.Text = "Show GPO names in Details pane";
			this.showGPONamesInDetailsPaneToolStripMenuItem.Click += new global::System.EventHandler(this.showGPONamesInDetailsPaneToolStripMenuItem_Click);
			this.clientSideExtensionsCSEsToolStripMenuItem.Name = "clientSideExtensionsCSEsToolStripMenuItem";
			this.clientSideExtensionsCSEsToolStripMenuItem.Size = new global::System.Drawing.Size(434, 38);
			this.clientSideExtensionsCSEsToolStripMenuItem.Text = "Client Side Extensions (CSEs)...";
			this.clientSideExtensionsCSEsToolStripMenuItem.Click += new global::System.EventHandler(this.clientSideExtensionsCSEsToolStripMenuItem_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(1321, 834);
			base.Controls.Add(this.splitContainer1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "PolicyViewer3";
			this.Text = "Policy Viewer";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x040007B4 RID: 1972
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040007B5 RID: 1973
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040007B6 RID: 1974
		private global::System.Windows.Forms.ToolStripDropDownButton clipboardToolStripDropDown;

		// Token: 0x040007B7 RID: 1975
		private global::System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;

		// Token: 0x040007B8 RID: 1976
		private global::System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;

		// Token: 0x040007B9 RID: 1977
		private global::System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton4;

		// Token: 0x040007BA RID: 1978
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x040007BB RID: 1979
		private global::System.Windows.Forms.ToolStripMenuItem showOnlyDifferencesToolStripMenuItem;

		// Token: 0x040007BC RID: 1980
		private global::System.Windows.Forms.ToolStripMenuItem showOnlyConflictToolStripMenuItem;

		// Token: 0x040007BD RID: 1981
		private global::System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;

		// Token: 0x040007BE RID: 1982
		private global::System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;

		// Token: 0x040007BF RID: 1983
		private global::System.Windows.Forms.ToolStripMenuItem findAgainToolStripMenuItem;

		// Token: 0x040007C0 RID: 1984
		private global::System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;

		// Token: 0x040007C1 RID: 1985
		private global::System.Windows.Forms.ToolStripMenuItem ExportToExcelToolStripMenuItem;

		// Token: 0x040007C2 RID: 1986
		private global::System.Windows.Forms.DataGridView dataGridView1;

		// Token: 0x040007C3 RID: 1987
		private global::System.Windows.Forms.DataGridViewTextBoxColumn colPolicyType;

		// Token: 0x040007C4 RID: 1988
		private global::System.Windows.Forms.DataGridViewTextBoxColumn colPolicyGroupOrRegKey;

		// Token: 0x040007C5 RID: 1989
		private global::System.Windows.Forms.DataGridViewTextBoxColumn colPolicySetting;

		// Token: 0x040007C6 RID: 1990
		private global::System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;

		// Token: 0x040007C7 RID: 1991
		private global::System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;

		// Token: 0x040007C8 RID: 1992
		private global::System.Windows.Forms.SplitContainer splitContainer1;

		// Token: 0x040007C9 RID: 1993
		private global::PolicyAnalyzer.MyRichTextBox rtDetails;

		// Token: 0x040007CA RID: 1994
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x040007CB RID: 1995
		private global::System.Windows.Forms.ToolStripMenuItem showDetailsPaneToolStripMenuItem1;

		// Token: 0x040007CC RID: 1996
		private global::System.Windows.Forms.ToolStripMenuItem gPOFilterToolStripMenuItem;

		// Token: 0x040007CD RID: 1997
		private global::System.Windows.Forms.ToolStripMenuItem showGPONamesInDetailsPaneToolStripMenuItem;

		// Token: 0x040007CE RID: 1998
		private global::System.Windows.Forms.ToolStripMenuItem showGPONamesAndFilesInDetailsPaneToolStripMenuItem;

		// Token: 0x040007CF RID: 1999
		private global::System.Windows.Forms.ToolStripMenuItem exportAllDataToExcelToolStripMenuItem;

		// Token: 0x040007D0 RID: 2000
		private global::System.Windows.Forms.ToolStripMenuItem showExplanationTextForSettingsToolStripMenuItem;

		// Token: 0x040007D1 RID: 2001
		private global::System.Windows.Forms.ToolStripMenuItem clientSideExtensionsCSEsToolStripMenuItem;

		// Token: 0x040007D2 RID: 2002
		private global::System.Windows.Forms.ToolStripMenuItem exportAllDataToCSVToolStripMenuItem;
	}
}
