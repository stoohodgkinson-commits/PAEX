using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SharedCode;
using WindowsFolderPicker;

namespace PolicyAnalyzer
{
	// Token: 0x02000078 RID: 120
	public partial class PolicyFileImporter : Form
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00003785 File Offset: 0x00001985
		public PolicyFileImporter(string sPolicyRulesFolder)
		{
			this.m_sPolicyRulesFolder = sPolicyRulesFolder;
			this.InitializeComponent();
			base.CenterToScreen();
			base.DialogResult = DialogResult.Cancel;
			LviTagComparer.PrepareHeaders(this.listView1);
			this.UpdateImportButton();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000037B8 File Offset: 0x000019B8
		// (set) Token: 0x060001EA RID: 490 RVA: 0x000037C0 File Offset: 0x000019C0
		public string m_sPolicyRulesName { get; private set; }

		// Token: 0x060001EB RID: 491 RVA: 0x000037C9 File Offset: 0x000019C9
		private void PolicyFileImporter_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000037C9 File Offset: 0x000019C9
		private void PolicyFileImporter_Shown(object sender, EventArgs e)
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000149AC File Offset: 0x00012BAC
		private void PolicyFileImporter_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.listView1.Items.Count > 0 && this.m_bDisplayWarningOnExit && DialogResult.Cancel == MessageBox.Show("Close this dialog without importing policies?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
			{
				e.Cancel = true;
				return;
			}
			base.DialogResult = (this.m_bPoliciesImported ? DialogResult.OK : DialogResult.Cancel);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000037CB File Offset: 0x000019CB
		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			LviTagComparer.SetComparer(this.listView1, e);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00014A0C File Offset: 0x00012C0C
		private void importGPOFilesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = string.Empty;
			NativeFolderPicker nativeFolderPicker = new NativeFolderPicker();
			nativeFolderPicker.Title = "Browse for the root folder under which the GPO(s) are stored.";
			nativeFolderPicker.FileNameLabel = "GPO root folder:";
			nativeFolderPicker.InitialFolder = Globals.sLastGpoFolder;
			if (!nativeFolderPicker.Show(base.Handle))
			{
				return;
			}
			text = (Globals.sLastGpoFolder = nativeFolderPicker.SelectedFolder);
			if (!Directory.Exists(text))
			{
				return;
			}
			ProcessRunning processRunning = new ProcessRunning(this.Text, "Getting policy file information from " + text + "...", 0, 100);
			processRunning.Show(this);
			processRunning.Update();
			string empty = string.Empty;
			List<GpoImportCode.GpoFileInfo> list;
			if (!GpoImportCode.IdentifyGpoFiles(text, out list, out empty))
			{
				processRunning.Close();
				processRunning.Dispose();
				MessageBox.Show(empty, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			foreach (GpoImportCode.GpoFileInfo gpoFileInfo in list)
			{
				ListViewItem listViewItem;
				if (gpoFileInfo.IsCSE())
				{
					listViewItem = new ListViewItem(new string[]
					{
						this.CseDialogPrefix() + gpoFileInfo.CSEName,
						gpoFileInfo.PolicyType,
						gpoFileInfo.CSEGuid,
						string.Empty
					});
				}
				else
				{
					listViewItem = new ListViewItem(new string[]
					{
						gpoFileInfo.PolicyName,
						gpoFileInfo.PolicyType,
						gpoFileInfo.PolicyFile.Name,
						gpoFileInfo.PolicyFile.DirectoryName
					});
				}
				this.listView1.Items.Add(listViewItem);
			}
			this.DoResize();
			processRunning.Close();
			processRunning.Dispose();
			this.UpdateImportButton();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00014BC0 File Offset: 0x00012DC0
		private void addSecurityTemplateinfToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.CheckFileExists = true;
			openFileDialog.Title = "Add Security Template file...";
			openFileDialog.Filter = "Security Templates|*.inf|All files (*.*)|*.*";
			openFileDialog.InitialDirectory = Globals.sLastGpoFolder;
			if (DialogResult.OK == openFileDialog.ShowDialog(this))
			{
				FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
				ListViewItem listViewItem = new ListViewItem(new string[]
				{
					this.PolicyNameLabel(),
					this.SecTemplateLabel(),
					fileInfo.Name,
					fileInfo.DirectoryName
				});
				this.listView1.Items.Add(listViewItem);
				listViewItem.BeginEdit();
				this.m_bDisplayWarningOnExit = true;
				this.UpdateImportButton();
				Globals.sLastGpoFolder = Path.GetDirectoryName(openFileDialog.FileName);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00014C78 File Offset: 0x00012E78
		private void addAuditPolicyauditcsvToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.CheckFileExists = true;
			openFileDialog.Title = "Add Audit Policy file...";
			openFileDialog.Filter = "Audit Policy|*.csv|All files (*.*)|*.*";
			openFileDialog.InitialDirectory = Globals.sLastGpoFolder;
			if (DialogResult.OK == openFileDialog.ShowDialog(this))
			{
				FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
				ListViewItem listViewItem = new ListViewItem(new string[]
				{
					this.PolicyNameLabel(),
					this.AuditPolicyLabel(),
					fileInfo.Name,
					fileInfo.DirectoryName
				});
				this.listView1.Items.Add(listViewItem);
				listViewItem.BeginEdit();
				this.m_bDisplayWarningOnExit = true;
				this.UpdateImportButton();
				Globals.sLastGpoFolder = Path.GetDirectoryName(openFileDialog.FileName);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00014D30 File Offset: 0x00012F30
		private void AddRegistryPol(string sConfigType)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.CheckFileExists = true;
			openFileDialog.Title = "Add " + sConfigType + " Configuration file...";
			openFileDialog.Filter = "Registry Policy files|*.pol|All files (*.*)|*.*";
			openFileDialog.InitialDirectory = Globals.sLastGpoFolder;
			if (DialogResult.OK == openFileDialog.ShowDialog(this))
			{
				FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
				ListViewItem listViewItem = new ListViewItem(new string[]
				{
					this.PolicyNameLabel(),
					sConfigType,
					fileInfo.Name,
					fileInfo.DirectoryName
				});
				this.listView1.Items.Add(listViewItem);
				listViewItem.BeginEdit();
				this.m_bDisplayWarningOnExit = true;
				this.UpdateImportButton();
				Globals.sLastGpoFolder = Path.GetDirectoryName(openFileDialog.FileName);
			}
			openFileDialog.Dispose();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000037D9 File Offset: 0x000019D9
		private void addComputerConfigurationFileregistrypolToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.AddRegistryPol(this.ComputerConfigLabel());
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000037E7 File Offset: 0x000019E7
		private void addUserConfigurationFileregistrypolToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.AddRegistryPol(this.UserConfigLabel());
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00014DF4 File Offset: 0x00012FF4
		private void inspectBackupxmlForCSEsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.CheckFileExists = true;
			openFileDialog.Title = "Inspect Backup.xml for CSEs...";
			openFileDialog.Filter = "Backup.xml|backup.xml|All files (*.*)|*.*";
			openFileDialog.InitialDirectory = Globals.sLastGpoFolder;
			if (DialogResult.OK == openFileDialog.ShowDialog(this))
			{
				List<GpoImportCode.GpoFileInfo> list;
				string text;
				if (GpoImportCode.ExtractCSEInfo(openFileDialog.FileName, out list, out text))
				{
					foreach (GpoImportCode.GpoFileInfo gpoFileInfo in list)
					{
						ListViewItem listViewItem = new ListViewItem(new string[]
						{
							this.CseDialogPrefix() + gpoFileInfo.CSEName,
							gpoFileInfo.PolicyType,
							gpoFileInfo.CSEGuid,
							string.Empty
						});
						this.listView1.Items.Add(listViewItem);
					}
				}
				this.DoResize();
				this.UpdateImportButton();
				Globals.sLastGpoFolder = Path.GetDirectoryName(openFileDialog.FileName);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00014EF8 File Offset: 0x000130F8
		private void btnImportChecked_Click(object sender, EventArgs e)
		{
			if (this.listView1.Items.Count == 0)
			{
				MessageBox.Show("No policy files have been selected for processing.  Add files to the policy set from the File menu.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.listView1.SelectedItems.Count > 1)
			{
				string text = "You have selected multiple items. Policy Analyzer imports ALL the files in the list, not just those that are highlighted.\r\n\r\nTo remove files from the list, select them and then press \"Del\" or choose Edit | Remove.\r\n\r\nClick \"OK\" to continue importing all files in the list.\r\nClick \"Cancel\" to edit the list.";
				if (DialogResult.Cancel == MessageBox.Show(text, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
				{
					return;
				}
			}
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.InitialDirectory = this.m_sPolicyRulesFolder;
			saveFileDialog.AddExtension = true;
			saveFileDialog.DefaultExt = PolicyRules.DefaultExtension();
			saveFileDialog.Filter = "Policy Rules|*.PolicyRules|All files|*.*";
			saveFileDialog.OverwritePrompt = true;
			saveFileDialog.Title = "Save imported Policy Rules...";
			DialogResult dialogResult = saveFileDialog.ShowDialog(this);
			string fileName = saveFileDialog.FileName;
			saveFileDialog.Dispose();
			if (DialogResult.OK != dialogResult)
			{
				return;
			}
			List<GpoImportCode.GpoFileInfo> list = new List<GpoImportCode.GpoFileInfo>();
			foreach (object obj in this.listView1.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				string text2 = listViewItem.SubItems[1].Text;
				bool flag;
				if (GpoImportCode.GpoFileInfo.IsCSELabel(text2, out flag))
				{
					string text3 = listViewItem.SubItems[2].Text;
					string text4 = GpoImportCode.CSEGuidToName(text3);
					list.Add(new GpoImportCode.GpoFileInfo(flag, text3, text4));
				}
				else
				{
					string text5 = Path.Combine(listViewItem.SubItems[3].Text, listViewItem.SubItems[2].Text);
					string text6 = listViewItem.SubItems[0].Text;
					list.Add(new GpoImportCode.GpoFileInfo(text2, text6, text5));
				}
			}
			string empty = string.Empty;
			ProcessRunning processRunning = new ProcessRunning(this.Text, "Importing policy files...", 0, 100);
			processRunning.Show(this);
			processRunning.Update();
			bool flag2 = GpoImportCode.BuildPolicyRulesFile(list, fileName, out empty);
			processRunning.Close();
			processRunning.Dispose();
			if (flag2)
			{
				this.m_sPolicyRulesName = Path.GetFileNameWithoutExtension(fileName);
			}
			else
			{
				MessageBox.Show(empty, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			this.m_bDisplayWarningOnExit = false;
			this.m_bPoliciesImported = true;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000035D1 File Offset: 0x000017D1
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00015134 File Offset: 0x00013334
		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder((this.listView1.SelectedItems.Count == 1) ? "Confirm removing the following policy file:\n\n" : "Confirm removing the following policy files:\n\n");
				foreach (object obj in this.listView1.SelectedItems)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					stringBuilder.AppendLine(listViewItem.SubItems[0].Text + " (" + listViewItem.SubItems[1].Text + ")");
				}
				if (DialogResult.OK != MessageBox.Show(stringBuilder.ToString(), this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
				{
					return;
				}
				foreach (object obj2 in this.listView1.SelectedItems)
				{
					((ListViewItem)obj2).Remove();
				}
				this.UpdateImportButton();
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00015268 File Offset: 0x00013468
		private void DoResize()
		{
			if (this.listView1.Items.Count > 0)
			{
				this.listView1.SuspendLayout();
				this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
				foreach (object obj in this.listView1.Columns)
				{
					ColumnHeader columnHeader = (ColumnHeader)obj;
					if (columnHeader.Width < 60)
					{
						columnHeader.Width = 60;
					}
				}
				this.listView1.ResumeLayout();
				this.m_bDisplayWarningOnExit = true;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000037F5 File Offset: 0x000019F5
		private void UpdateImportButton()
		{
			this.btnImport.Enabled = this.listView1.Items.Count > 0;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00003815 File Offset: 0x00001A15
		private void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				this.listView1.SelectedItems[0].BeginEdit();
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00015310 File Offset: 0x00013510
		private void listView1_KeyUp(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Delete)
			{
				if (keyCode == Keys.F2)
				{
					this.renameToolStripMenuItem_Click(sender, null);
					return;
				}
			}
			else
			{
				this.deleteToolStripMenuItem_Click(sender, null);
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00003840 File Offset: 0x00001A40
		private string ComputerConfigLabel()
		{
			return GpoImportCode.ComputerConfigLabel();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00003847 File Offset: 0x00001A47
		private string UserConfigLabel()
		{
			return GpoImportCode.UserConfigLabel();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000384E File Offset: 0x00001A4E
		private string SecTemplateLabel()
		{
			return GpoImportCode.SecTemplateLabel();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00003855 File Offset: 0x00001A55
		private string AuditPolicyLabel()
		{
			return GpoImportCode.AuditPolicyLabel();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000385C File Offset: 0x00001A5C
		private string PolicyNameLabel()
		{
			return "(Policy name...)";
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00003863 File Offset: 0x00001A63
		private string CseDialogPrefix()
		{
			return "*** Group policy client-side extension: ";
		}

		// Token: 0x0400078E RID: 1934
		private bool m_bDisplayWarningOnExit;

		// Token: 0x0400078F RID: 1935
		private bool m_bPoliciesImported;

		// Token: 0x04000790 RID: 1936
		private string m_sPolicyRulesFolder;
	}
}
