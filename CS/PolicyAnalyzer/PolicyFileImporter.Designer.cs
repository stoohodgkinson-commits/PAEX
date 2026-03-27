namespace PolicyAnalyzer
{
	// Token: 0x02000078 RID: 120
	public partial class PolicyFileImporter : global::System.Windows.Forms.Form
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0000386A File Offset: 0x00001A6A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00015340 File Offset: 0x00013540
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PolicyAnalyzer.PolicyFileImporter));
			this.menuStrip1 = new global::System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.importGPOFilesToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.addComputerConfigurationFileregistrypolToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.addUserConfigurationFileregistrypolToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.addSecurityTemplateinfToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.addAuditPolicyauditcsvToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.listView1 = new global::System.Windows.Forms.ListView();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new global::System.Windows.Forms.ColumnHeader();
			this.btnImport = new global::System.Windows.Forms.Button();
			this.inspectBackupxmlForCSEsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.menuStrip1.ImageScalingSize = new global::System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.fileToolStripMenuItem, this.editToolStripMenuItem });
			this.menuStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new global::System.Windows.Forms.Padding(12, 3, 0, 3);
			this.menuStrip1.Size = new global::System.Drawing.Size(1448, 44);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.importGPOFilesToolStripMenuItem, this.toolStripSeparator1, this.addComputerConfigurationFileregistrypolToolStripMenuItem, this.addUserConfigurationFileregistrypolToolStripMenuItem, this.addSecurityTemplateinfToolStripMenuItem, this.addAuditPolicyauditcsvToolStripMenuItem, this.inspectBackupxmlForCSEsToolStripMenuItem, this.toolStripSeparator2, this.exitToolStripMenuItem });
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new global::System.Drawing.Size(64, 38);
			this.fileToolStripMenuItem.Text = "File";
			this.importGPOFilesToolStripMenuItem.Name = "importGPOFilesToolStripMenuItem";
			this.importGPOFilesToolStripMenuItem.Size = new global::System.Drawing.Size(579, 38);
			this.importGPOFilesToolStripMenuItem.Text = "Add files from GPO(s)...";
			this.importGPOFilesToolStripMenuItem.Click += new global::System.EventHandler(this.importGPOFilesToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(576, 6);
			this.addComputerConfigurationFileregistrypolToolStripMenuItem.Name = "addComputerConfigurationFileregistrypolToolStripMenuItem";
			this.addComputerConfigurationFileregistrypolToolStripMenuItem.Size = new global::System.Drawing.Size(579, 38);
			this.addComputerConfigurationFileregistrypolToolStripMenuItem.Text = "Add Computer Configuration (registry.pol)...";
			this.addComputerConfigurationFileregistrypolToolStripMenuItem.Click += new global::System.EventHandler(this.addComputerConfigurationFileregistrypolToolStripMenuItem_Click);
			this.addUserConfigurationFileregistrypolToolStripMenuItem.Name = "addUserConfigurationFileregistrypolToolStripMenuItem";
			this.addUserConfigurationFileregistrypolToolStripMenuItem.Size = new global::System.Drawing.Size(579, 38);
			this.addUserConfigurationFileregistrypolToolStripMenuItem.Text = "Add User Configuration (registry.pol)...";
			this.addUserConfigurationFileregistrypolToolStripMenuItem.Click += new global::System.EventHandler(this.addUserConfigurationFileregistrypolToolStripMenuItem_Click);
			this.addSecurityTemplateinfToolStripMenuItem.Name = "addSecurityTemplateinfToolStripMenuItem";
			this.addSecurityTemplateinfToolStripMenuItem.Size = new global::System.Drawing.Size(579, 38);
			this.addSecurityTemplateinfToolStripMenuItem.Text = "Add Security Template (*.inf)...";
			this.addSecurityTemplateinfToolStripMenuItem.Click += new global::System.EventHandler(this.addSecurityTemplateinfToolStripMenuItem_Click);
			this.addAuditPolicyauditcsvToolStripMenuItem.Name = "addAuditPolicyauditcsvToolStripMenuItem";
			this.addAuditPolicyauditcsvToolStripMenuItem.Size = new global::System.Drawing.Size(579, 38);
			this.addAuditPolicyauditcsvToolStripMenuItem.Text = "Add Audit Policy (audit.csv)...";
			this.addAuditPolicyauditcsvToolStripMenuItem.Click += new global::System.EventHandler(this.addAuditPolicyauditcsvToolStripMenuItem_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(576, 6);
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new global::System.Drawing.Size(579, 38);
			this.exitToolStripMenuItem.Text = "Close";
			this.exitToolStripMenuItem.Click += new global::System.EventHandler(this.exitToolStripMenuItem_Click);
			this.editToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[] { this.deleteToolStripMenuItem, this.renameToolStripMenuItem });
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new global::System.Drawing.Size(67, 38);
			this.editToolStripMenuItem.Text = "Edit";
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new global::System.Drawing.Size(270, 38);
			this.deleteToolStripMenuItem.Text = "Remove";
			this.deleteToolStripMenuItem.Click += new global::System.EventHandler(this.deleteToolStripMenuItem_Click);
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.Size = new global::System.Drawing.Size(270, 38);
			this.renameToolStripMenuItem.Text = "Rename policy";
			this.renameToolStripMenuItem.Click += new global::System.EventHandler(this.renameToolStripMenuItem_Click);
			this.listView1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.listView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4 });
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.LabelEdit = true;
			this.listView1.Location = new global::System.Drawing.Point(24, 52);
			this.listView1.Margin = new global::System.Windows.Forms.Padding(6);
			this.listView1.Name = "listView1";
			this.listView1.Size = new global::System.Drawing.Size(1392, 496);
			this.listView1.TabIndex = 5;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = global::System.Windows.Forms.View.Details;
			this.listView1.ColumnClick += new global::System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			this.listView1.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.listView1_KeyUp);
			this.columnHeader1.Text = "Policy Name";
			this.columnHeader1.Width = 87;
			this.columnHeader2.Text = "Policy Type";
			this.columnHeader2.Width = 83;
			this.columnHeader3.Text = "File name";
			this.columnHeader3.Width = 92;
			this.columnHeader4.Text = "Folder";
			this.columnHeader4.Width = 427;
			this.btnImport.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.btnImport.Enabled = false;
			this.btnImport.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnImport.Location = new global::System.Drawing.Point(452, 564);
			this.btnImport.Margin = new global::System.Windows.Forms.Padding(6);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new global::System.Drawing.Size(546, 111);
			this.btnImport.TabIndex = 9;
			this.btnImport.Text = "Import...";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new global::System.EventHandler(this.btnImportChecked_Click);
			this.inspectBackupxmlForCSEsToolStripMenuItem.Name = "inspectBackupxmlForCSEsToolStripMenuItem";
			this.inspectBackupxmlForCSEsToolStripMenuItem.Size = new global::System.Drawing.Size(579, 38);
			this.inspectBackupxmlForCSEsToolStripMenuItem.Text = "Inspect Backup.xml for CSEs...";
			this.inspectBackupxmlForCSEsToolStripMenuItem.Click += new global::System.EventHandler(this.inspectBackupxmlForCSEsToolStripMenuItem_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(12f, 25f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(1448, 683);
			base.Controls.Add(this.btnImport);
			base.Controls.Add(this.listView1);
			base.Controls.Add(this.menuStrip1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip1;
			base.Margin = new global::System.Windows.Forms.Padding(6);
			this.MinimumSize = new global::System.Drawing.Size(1442, 677);
			base.Name = "PolicyFileImporter";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Show;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Policy File Importer";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.PolicyFileImporter_FormClosing);
			base.Load += new global::System.EventHandler(this.PolicyFileImporter_Load);
			base.Shown += new global::System.EventHandler(this.PolicyFileImporter_Shown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000792 RID: 1938
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000793 RID: 1939
		private global::System.Windows.Forms.MenuStrip menuStrip1;

		// Token: 0x04000794 RID: 1940
		private global::System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;

		// Token: 0x04000795 RID: 1941
		private global::System.Windows.Forms.ToolStripMenuItem importGPOFilesToolStripMenuItem;

		// Token: 0x04000796 RID: 1942
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000797 RID: 1943
		private global::System.Windows.Forms.ToolStripMenuItem addComputerConfigurationFileregistrypolToolStripMenuItem;

		// Token: 0x04000798 RID: 1944
		private global::System.Windows.Forms.ToolStripMenuItem addUserConfigurationFileregistrypolToolStripMenuItem;

		// Token: 0x04000799 RID: 1945
		private global::System.Windows.Forms.ToolStripMenuItem addSecurityTemplateinfToolStripMenuItem;

		// Token: 0x0400079A RID: 1946
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x0400079B RID: 1947
		private global::System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

		// Token: 0x0400079C RID: 1948
		private global::System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;

		// Token: 0x0400079D RID: 1949
		private global::System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;

		// Token: 0x0400079E RID: 1950
		private global::System.Windows.Forms.ListView listView1;

		// Token: 0x0400079F RID: 1951
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x040007A0 RID: 1952
		private global::System.Windows.Forms.ColumnHeader columnHeader2;

		// Token: 0x040007A1 RID: 1953
		private global::System.Windows.Forms.ColumnHeader columnHeader3;

		// Token: 0x040007A2 RID: 1954
		private global::System.Windows.Forms.ColumnHeader columnHeader4;

		// Token: 0x040007A3 RID: 1955
		private global::System.Windows.Forms.Button btnImport;

		// Token: 0x040007A4 RID: 1956
		private global::System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;

		// Token: 0x040007A5 RID: 1957
		private global::System.Windows.Forms.ToolStripMenuItem addAuditPolicyauditcsvToolStripMenuItem;

		// Token: 0x040007A6 RID: 1958
		private global::System.Windows.Forms.ToolStripMenuItem inspectBackupxmlForCSEsToolStripMenuItem;
	}
}
