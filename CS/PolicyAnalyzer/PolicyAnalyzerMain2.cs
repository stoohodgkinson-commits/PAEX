using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using GPLookup;
using SharedCode;
using WindowsFolderPicker;

namespace PolicyAnalyzer
{
	// Token: 0x02000077 RID: 119
	public partial class PolicyAnalyzerMain2 : Form
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x000131F4 File Offset: 0x000113F4
		public PolicyAnalyzerMain2()
		{
			this.InitializeComponent();
			this.Text = this.Text + " v" + Utils.AppVersionFullString();
			try
			{
				Directory.CreateDirectory(PolicyAnalyzerDataFolder.DefaultFolderPath());
			}
			catch (Exception)
			{
			}
			this.btnPolicyRulesFolder.Text = Globals.sLastPolicyRulesFolder;
			this.btnAdmxPath.Text = Globals.sAdmxPath;
			LviTagComparer.PrepareHeaders(this.listView1);
			this.toolTip1.SetToolTip(this.btnPolicyRulesFolder, "Click to change Policy Rules directory");
			this.toolTip1.SetToolTip(this.btnResetPolicyRulesPath, "Use default Policy Rules directory");
			this.toolTip1.SetToolTip(this.btnAdmxPath, "Click to change ADMX-files directory");
			this.toolTip1.SetToolTip(this.btnResetPolicyDefsPath, "Use default ADMX-files directory");
			this.toolTip1.SetToolTip(this.btnImport, "Import Policy Rules from Group Policy files");
			Size smallIconSize = SystemInformation.SmallIconSize;
			Bitmap bitmap = new Bitmap(smallIconSize.Width, smallIconSize.Height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.DrawImage(SystemIcons.Shield.ToBitmap(), new Rectangle(Point.Empty, smallIconSize));
			}
			this.btnCompareToEffectiveState.Image = bitmap;
			this.btnCompareToEffectiveState.ImageAlign = ContentAlignment.BottomCenter;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000036CA File Offset: 0x000018CA
		private void PolicyAnalyzerMain2_Load(object sender, EventArgs e)
		{
			this.ReloadRulesList(false, "");
			this.UpdateSelectionUI();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000036DE File Offset: 0x000018DE
		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			LviTagComparer.SetComparer(this.listView1, e);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00013358 File Offset: 0x00011558
		private void btnPolicyRulesFolder_Click(object sender, EventArgs e)
		{
			NativeFolderPicker nativeFolderPicker = new NativeFolderPicker();
			nativeFolderPicker.Title = "Pick the folder containing the Policy Analyzer Policy Rules files";
			nativeFolderPicker.FileNameLabel = "Folder containing PolicyRules files";
			nativeFolderPicker.InitialFolder = Globals.sLastPolicyRulesFolder;
			if (nativeFolderPicker.Show(base.Handle))
			{
				this.btnPolicyRulesFolder.Text = (Globals.sLastPolicyRulesFolder = nativeFolderPicker.SelectedFolder);
				Globals.SaveConfig();
				this.ReloadRulesList(false, "");
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000036EC File Offset: 0x000018EC
		private void btnResetPolicyRulesPath_Click(object sender, EventArgs e)
		{
			this.btnPolicyRulesFolder.Text = (Globals.sLastPolicyRulesFolder = PolicyAnalyzerDataFolder.DefaultFolderPath());
			Globals.SaveConfig();
			this.ReloadRulesList(false, "");
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000133C4 File Offset: 0x000115C4
		private void btnAdmxPath_Click(object sender, EventArgs e)
		{
			NativeFolderPicker nativeFolderPicker = new NativeFolderPicker();
			nativeFolderPicker.Title = "Pick the directory containing the Windows Policy Definitions (ADMX) files";
			nativeFolderPicker.FileNameLabel = "Directory containing Policy Definitions (ADMX) files";
			nativeFolderPicker.InitialFolder = Globals.sAdmxPath;
			if (nativeFolderPicker.Show(base.Handle))
			{
				this.SetAdmxPath(nativeFolderPicker.SelectedFolder);
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00003715 File Offset: 0x00001915
		private void btnResetPolicyDefsPath_Click(object sender, EventArgs e)
		{
			this.SetAdmxPath(Path.Combine(Environment.GetEnvironmentVariable("windir"), "PolicyDefinitions"));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00003731 File Offset: 0x00001931
		private void SetAdmxPath(string sAdmxPath)
		{
			this.gpLookup = null;
			Control control = this.btnAdmxPath;
			Globals.sAdmxPath = sAdmxPath;
			control.Text = sAdmxPath;
			Globals.SaveConfig();
			GPLookup_t.Reinitialize();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00013414 File Offset: 0x00011614
		private void btnImport_Click(object sender, EventArgs e)
		{
			PolicyFileImporter policyFileImporter = new PolicyFileImporter(Globals.sLastPolicyRulesFolder);
			DialogResult dialogResult = policyFileImporter.ShowDialog(this);
			if (DialogResult.OK == dialogResult)
			{
				this.ReloadRulesList(true, policyFileImporter.m_sPolicyRulesName);
			}
			policyFileImporter.Dispose();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0001344C File Offset: 0x0001164C
		private void btnCompare3_Click(object sender, EventArgs e)
		{
			int count = this.listView1.CheckedItems.Count;
			int num = count;
			if (num == 0)
			{
				MessageBox.Show("No rule sets are checked.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			NameAndPolicyRules_t[] array = new NameAndPolicyRules_t[num];
			int num2 = 0;
			string text = PolicyRules.DefaultExtension();
			int i = 0;
			while (i < count)
			{
				ListViewItem listViewItem = this.listView1.CheckedItems[i];
				string text2 = Path.Combine(Globals.sLastPolicyRulesFolder, listViewItem.Text + "." + text);
				PolicyRules policyRules = new PolicyRules(this.GetGpRules(), PolicyRules.OverrideBehavior_t.eMultipleSettingsAllowed);
				string text3;
				if (!policyRules.LoadFromFile(text2, false, out text3))
				{
					MessageBox.Show(text3, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				array[num2] = default(NameAndPolicyRules_t);
				array[num2].m_sName = listViewItem.Text;
				array[num2].m_rules = policyRules;
				i++;
				num2++;
			}
			new PolicyViewer3(array, this.GetGpRules()).Show();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0001354C File Offset: 0x0001174C
		private void btnCompareToEffectiveState_Click(object sender, EventArgs e)
		{
			int count = this.listView1.CheckedItems.Count;
			if (count == 0)
			{
				MessageBox.Show("No rule sets are checked.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			NameAndPolicyRules_t[] array = new NameAndPolicyRules_t[2];
			PolicyRules policyRules = new PolicyRules(this.GetGpRules(), PolicyRules.OverrideBehavior_t.eMultipleSettingsAllowed);
			BaselineEvaluator baselineEvaluator = new BaselineEvaluator();
			string empty = string.Empty;
			string text = PolicyRules.DefaultExtension();
			for (int i = 0; i < count; i++)
			{
				ListViewItem listViewItem = this.listView1.CheckedItems[i];
				string text2 = Path.Combine(Globals.sLastPolicyRulesFolder, listViewItem.Text + "." + text);
				if (!policyRules.LoadFromFile(text2, i > 0, out empty))
				{
					MessageBox.Show(empty + "; " + text2, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (!baselineEvaluator.AddBaselineDoc(text2, out empty))
				{
					MessageBox.Show(empty + "; " + text2, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (empty.Length > 0)
				{
					MessageBox.Show(empty + "; " + text2, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			string text3 = "EffectiveState_" + Environment.MachineName + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
			string text4 = Path.Combine(Globals.sLastPolicyRulesFolder, text3 + "." + PolicyRules.DefaultExtension());
			string empty2 = string.Empty;
			if (!baselineEvaluator.EvaluateEffectiveState(text4, out empty2))
			{
				MessageBox.Show(empty2, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (empty2.Length > 0)
			{
				MessageBox.Show(empty2, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			this.ReloadRulesList(true, "");
			PolicyRules policyRules2 = new PolicyRules(this.GetGpRules(), PolicyRules.OverrideBehavior_t.eMultipleSettingsAllowed);
			if (!policyRules2.LoadFromFile(text4, false, out empty))
			{
				MessageBox.Show(empty + "; " + text4, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			array[0] = default(NameAndPolicyRules_t);
			array[0].m_sName = "Baseline(s)";
			array[0].m_rules = policyRules;
			array[1] = default(NameAndPolicyRules_t);
			array[1].m_sName = "Effective state";
			array[1].m_rules = policyRules2;
			new PolicyViewer3(array, this.GetGpRules()).Show();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000137A4 File Offset: 0x000119A4
		private void ReloadRulesList(bool bRetainChecks = false, string sAddCheck = "")
		{
			List<string> list = new List<string>();
			if (sAddCheck.Length > 0)
			{
				list.Add(sAddCheck);
			}
			if (bRetainChecks)
			{
				foreach (object obj in this.listView1.CheckedItems)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					list.Add(listViewItem.Text);
				}
			}
			this.listView1.Items.Clear();
			DirectoryInfo directoryInfo = new DirectoryInfo(Globals.sLastPolicyRulesFolder);
			if (!directoryInfo.Exists)
			{
				this.btnPolicyRulesFolder.Text = (Globals.sLastPolicyRulesFolder = PolicyAnalyzerDataFolder.DefaultFolderPath());
				directoryInfo = new DirectoryInfo(Globals.sLastPolicyRulesFolder);
				if (!directoryInfo.Exists)
				{
					MessageBox.Show("Warning: Initial policy rules directory \"" + Globals.sLastPolicyRulesFolder + "\" does not exist. Click the directory name to change to a different directory.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			FileInfo[] files = directoryInfo.GetFiles("*.PolicyRules", SearchOption.TopDirectoryOnly);
			foreach (FileInfo fileInfo in files)
			{
				DateTime lastWriteTime = fileInfo.LastWriteTime;
				ListViewItem.ListViewSubItem[] array2 = new ListViewItem.ListViewSubItem[3];
				string text = fileInfo.Name.Substring(0, fileInfo.Name.Length - 12);
				array2[0] = new ListViewItem.ListViewSubItem();
				array2[1] = new ListViewItem.ListViewSubItem();
				array2[2] = new ListViewItem.ListViewSubItem();
				array2[0].Text = text;
				array2[0].Tag = text;
				array2[1].Text = lastWriteTime.ToString("G");
				array2[1].Tag = lastWriteTime;
				array2[2].Text = fileInfo.Length.ToString("N0");
				array2[2].Tag = fileInfo.Length;
				ListViewItem listViewItem2 = new ListViewItem(array2, 0);
				if (list.Contains(text))
				{
					listViewItem2.Checked = true;
				}
				this.listView1.Items.Add(listViewItem2);
			}
			if (files.Length != 0)
			{
				this.listView1.SuspendLayout();
				this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
				foreach (object obj2 in this.listView1.Columns)
				{
					ColumnHeader columnHeader = (ColumnHeader)obj2;
					if (columnHeader.Width < 60)
					{
						columnHeader.Width = 60;
					}
				}
				this.listView1.Items[0].Selected = true;
				this.listView1.ResumeLayout();
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00013A40 File Offset: 0x00011C40
		private GPLookup_t GetGpRules()
		{
			if (this.gpLookup == null)
			{
				ProcessRunning processRunning = new ProcessRunning(this.Text, "Loading Group Policy data...", 0, 100);
				GPLookup_t.st_ProgIndicatorDelegate = new GPLookup_t.ProgressIndicator(ProcessRunning.Progress);
				GPLookup_t.AdmxDirectory = Globals.sAdmxPath;
				processRunning.Show(this);
				processRunning.Update();
				string empty = string.Empty;
				try
				{
					this.gpLookup = GPLookup_t.GPLookup(out empty);
				}
				catch
				{
				}
				if (empty.Length > 0)
				{
					MessageBox.Show(empty, "GPLookup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				processRunning.Close();
				processRunning.Dispose();
				GPLookup_t.st_ProgIndicatorDelegate = null;
			}
			return this.gpLookup;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00013AEC File Offset: 0x00011CEC
		private void DeleteCheckedItems()
		{
			if (this.listView1.CheckedItems.Count == 0)
			{
				MessageBox.Show("No Policy Rules are selected.\n\nTo delete Policy Rules files, check one or more items in the list and press the \"Del\" key again.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			List<string> list = new List<string>();
			foreach (object obj in this.listView1.CheckedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				stringBuilder.AppendLine(listViewItem.Text);
				list.Add(Path.Combine(Globals.sLastPolicyRulesFolder, listViewItem.Text + "." + PolicyRules.DefaultExtension()));
			}
			DialogResult dialogResult = MessageBox.Show("Confirm the PERMANENT deletion of the following Policy Rule sets in \"" + Globals.sLastPolicyRulesFolder + "\":\n\n" + stringBuilder.ToString(), this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
			if (DialogResult.OK != dialogResult)
			{
				return;
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (string text in list)
			{
				try
				{
					if (File.Exists(text))
					{
						File.Delete(text);
					}
				}
				catch (Exception ex)
				{
					stringBuilder2.AppendLine(text + ":  " + ex.Message + "\n");
				}
			}
			if (stringBuilder2.Length > 0)
			{
				MessageBox.Show("Unable to delete the following:\n\n" + stringBuilder2.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			this.ReloadRulesList(false, "");
			this.UpdateSelectionUI();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00003756 File Offset: 0x00001956
		private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			this.UpdateSelectionUI();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00013CA0 File Offset: 0x00011EA0
		private void UpdateSelectionUI()
		{
			this.bIsUpdating = true;
			int count = this.listView1.CheckedItems.Count;
			this.lblNSelected.Text = count.ToString() + " selected";
			this.btnCompare3.Enabled = (this.btnCompareToEffectiveState.Enabled = count > 0);
			this.btn_DeleteSelected.Enabled = count > 0;
			if (count == 0)
			{
				this.chkSelectAll.CheckState = CheckState.Unchecked;
			}
			else if (this.listView1.Items.Count == count)
			{
				this.chkSelectAll.CheckState = CheckState.Checked;
			}
			else
			{
				this.chkSelectAll.CheckState = CheckState.Indeterminate;
			}
			this.bIsUpdating = false;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00013D54 File Offset: 0x00011F54
		private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.bIsUpdating)
			{
				bool @checked = this.chkSelectAll.Checked;
				this.listView1.SuspendLayout();
				for (int i = 0; i < this.listView1.Items.Count; i++)
				{
					this.listView1.Items[i].Checked = @checked;
				}
				this.listView1.ResumeLayout();
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00013DC0 File Offset: 0x00011FC0
		private void listView1_KeyUp(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.Delete)
			{
				this.DeleteCheckedItems();
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000375E File Offset: 0x0000195E
		private void btn_DeleteSelected_Click(object sender, EventArgs e)
		{
			this.DeleteCheckedItems();
		}

		// Token: 0x0400077A RID: 1914
		private GPLookup_t gpLookup;

		// Token: 0x0400077B RID: 1915
		private bool bIsUpdating;
	}
}
