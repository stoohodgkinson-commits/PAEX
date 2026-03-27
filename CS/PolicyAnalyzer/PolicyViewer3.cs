using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using GPLookup;

namespace PolicyAnalyzer
{
	// Token: 0x02000079 RID: 121
	public partial class PolicyViewer3 : Form
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00015C78 File Offset: 0x00013E78
		public PolicyViewer3(NameAndPolicyRules_t[] nameAndPolicyRules, GPLookup_t gpLookup)
		{
			this.InitializeComponent();
			this.showDetailsPaneToolStripMenuItem1.Checked = this.m_ShowDetailsPane;
			if (!this.m_ShowDetailsPane)
			{
				this.ShowHideDetailsPane();
			}
			this.showExplanationTextForSettingsToolStripMenuItem.Checked = this.m_bShowExplainText;
			this.showGPONamesInDetailsPaneToolStripMenuItem.Checked = this.m_bShowGPOsInDetails;
			this.showGPONamesAndFilesInDetailsPaneToolStripMenuItem.Checked = this.m_bShowGPOsAndFilesInDetails;
			this.m_sBaseTitle = this.Text;
			this.m_compareCount = nameAndPolicyRules.GetLength(0);
			if (this.m_compareCount == 0)
			{
				return;
			}
			for (int i = 0; i < this.m_compareCount; i++)
			{
				NameAndPolicyRules_t nameAndPolicyRules_t = nameAndPolicyRules[i];
				this.dataGridView1.Columns.Add("colCompare" + i.ToString(), nameAndPolicyRules_t.m_sName);
			}
			this.m_policyCollection.LoadData(gpLookup, nameAndPolicyRules);
			this.Redisplay();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00015DA8 File Offset: 0x00013FA8
		private void Redisplay()
		{
			this.dataGridView1.Rows.Clear();
			Color color = Color.FromArgb(240, 240, 240);
			foreach (string text in this.m_policyCollection.Keys())
			{
				PolicyItemCollection_t policyItemCollection_t = this.m_policyCollection.KeyLookup(text);
				DataGridViewRow dataGridViewRow = (DataGridViewRow)this.dataGridView1.RowTemplate.Clone();
				dataGridViewRow.CreateCells(this.dataGridView1);
				dataGridViewRow.Tag = policyItemCollection_t;
				dataGridViewRow.Cells[0].Value = policyItemCollection_t.m_sPolType;
				dataGridViewRow.Cells[1].Value = policyItemCollection_t.m_sPolGroupOrKey;
				dataGridViewRow.Cells[2].Value = (policyItemCollection_t.m_bDeleteAllValues ? "[[[Delete all values]]]" : policyItemCollection_t.m_sPolSetting);
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				for (int i = 0; i < this.m_compareCount; i++)
				{
					PolicyItemInContext_t policyItemInContext_t = policyItemCollection_t.PolicyItemInContext(i);
					dataGridViewRow.Cells[3 + i].Value = policyItemInContext_t.DisplayText;
					Color color2 = dataGridViewRow.Cells[0].Style.BackColor;
					switch (policyItemInContext_t.InSetResult)
					{
					case eInSetResult_t.eNoSetting:
						flag3 = true;
						color2 = Color.DarkGray;
						break;
					case eInSetResult_t.eSingleSetting:
					case eInSetResult_t.eError:
						flag2 = true;
						break;
					case eInSetResult_t.eDuplicateSetting:
						flag2 = true;
						color2 = color;
						break;
					case eInSetResult_t.eDuplicateDelete:
						color2 = color;
						break;
					case eInSetResult_t.eConflictingSettings:
						flag = true;
						flag2 = true;
						color2 = Color.Yellow;
						break;
					}
					dataGridViewRow.Cells[3 + i].Style.BackColor = color2;
				}
				bool flag4 = true;
				PolicyViewer3.eShowCompare_t show = this.m_show;
				if (show != PolicyViewer3.eShowCompare_t.esc_ShowOnlyDifferences)
				{
					if (show == PolicyViewer3.eShowCompare_t.esc_ShowOnlyConflicts)
					{
						flag4 = flag;
					}
				}
				else
				{
					flag4 = flag || (flag2 && flag3 && !policyItemCollection_t.m_bDeleteAllValues);
				}
				if (flag4)
				{
					this.dataGridView1.Rows.Add(dataGridViewRow);
				}
			}
			this.Text = this.m_sBaseTitle + " - " + this.dataGridView1.Rows.Count.ToString() + " items";
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00003889 File Offset: 0x00001A89
		private void dataGridView1_SelectionChanged(object sender, EventArgs e)
		{
			this.RefreshDetailsPane();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0001601C File Offset: 0x0001421C
		private void RefreshDetailsPane()
		{
			if (!this.m_ShowDetailsPane)
			{
				return;
			}
			this.rtDetails.Clear();
			this.rtDetails.SetTabs(80);
			if (this.dataGridView1.SelectedRows.Count == 0)
			{
				return;
			}
			if (this.dataGridView1.SelectedRows.Count > 1)
			{
				return;
			}
			DataGridViewRow dataGridViewRow = this.dataGridView1.SelectedRows[0];
			PolicyItemCollection_t policyItemCollection_t = (PolicyItemCollection_t)dataGridViewRow.Tag;
			int count = policyItemCollection_t.m_polPaths.Count;
			if (count != 0)
			{
				if (count != 1)
				{
					this.rtDetails.AppendHeadline("Policy Paths:", true);
					using (List<PolicyPath_t>.Enumerator enumerator = policyItemCollection_t.m_polPaths.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							PolicyPath_t policyPath_t = enumerator.Current;
							this.rtDetails.AppendText(policyPath_t.m_sPolConfig + " | ", false);
							if (policyPath_t.m_sPolPath.Length > 0)
							{
								this.rtDetails.AppendText(policyPath_t.m_sPolPath + " | ", false);
							}
							if (policyPath_t.m_sPolNameSubOption.Length > 0)
							{
								this.rtDetails.AppendText(policyPath_t.m_sPolName + " --> " + policyPath_t.m_sPolNameSubOption, true);
							}
							else
							{
								this.rtDetails.AppendText(policyPath_t.m_sPolName, true);
							}
							if (this.m_bShowExplainText && policyPath_t.m_sExplainText.Length > 0)
							{
								this.rtDetails.AppendItalicBlock(policyPath_t.ExplainTextFormatted(), true);
							}
						}
						goto IL_02D1;
					}
				}
				this.rtDetails.AppendHeadline("Policy Path:", true);
				PolicyPath_t policyPath_t2 = policyItemCollection_t.m_polPaths[0];
				this.rtDetails.AppendText(policyPath_t2.m_sPolConfig, true);
				if (policyPath_t2.m_sPolPath.Length > 0)
				{
					this.rtDetails.AppendText(policyPath_t2.m_sPolPath, true);
				}
				if (policyPath_t2.m_sPolNameSubOption.Length > 0)
				{
					this.rtDetails.AppendText(policyPath_t2.m_sPolName + " --> " + policyPath_t2.m_sPolNameSubOption, true);
				}
				else
				{
					this.rtDetails.AppendText(policyPath_t2.m_sPolName, true);
				}
				if (this.m_bShowExplainText && policyPath_t2.m_sExplainText.Length > 0)
				{
					this.rtDetails.AppendText(string.Empty, true);
					this.rtDetails.AppendItalicBlock(policyPath_t2.ExplainTextFormatted(), true);
				}
			}
			else
			{
				this.rtDetails.AppendHeadline("Policy Path:", true);
				this.rtDetails.AppendText(dataGridViewRow.Cells[0].Value.ToString(), true);
				this.rtDetails.AppendText(dataGridViewRow.Cells[1].Value.ToString(), true);
				this.rtDetails.AppendText(dataGridViewRow.Cells[2].Value.ToString(), true);
			}
			IL_02D1:
			this.rtDetails.AppendText(string.Empty, true);
			for (int i = 0; i < this.m_compareCount; i++)
			{
				this.rtDetails.AppendHeadline(this.dataGridView1.Columns[3 + i].HeaderText + ":", true);
				SortedList<string, PolicyRules.PolicySettingData_t> sortedList = policyItemCollection_t.m_settingData[i];
				if (sortedList == null)
				{
					this.rtDetails.AppendText("Not specified", true);
					this.rtDetails.AppendText("", true);
				}
				else
				{
					PolicyItemInContext_t policyItemInContext_t = policyItemCollection_t.PolicyItemInContext(i);
					switch (policyItemInContext_t.InItemResult)
					{
					case eInSetResult_t.eNoSetting:
						this.rtDetails.AppendText("Not specified", true);
						break;
					case eInSetResult_t.eSingleSetting:
					case eInSetResult_t.eSingleDelete:
					{
						string text = policyItemCollection_t.FormattedData(policyItemInContext_t.representativeData).Replace("\r\n", "\v");
						this.rtDetails.AppendBoldItalicAndNormalText("Option:\t", text, true);
						if (text != policyItemInContext_t.DisplayText)
						{
							this.rtDetails.AppendBoldItalicAndNormalText("Data:\t", policyItemInContext_t.DisplayText, true);
						}
						if (policyItemInContext_t.representativeData.SettingType.Length > 0)
						{
							this.rtDetails.AppendBoldItalicAndNormalText("Type:\t", policyItemInContext_t.representativeData.SettingType, true);
						}
						if (this.m_bShowGPOsInDetails)
						{
							this.rtDetails.AppendBoldItalicAndNormalText("GPO:\t", policyItemInContext_t.representativeData.GpoName, true);
							if (this.m_bShowGPOsAndFilesInDetails)
							{
								this.rtDetails.AppendBoldItalicAndNormalText("File:\t", policyItemInContext_t.representativeData.SourceFile, true);
							}
						}
						this.rtDetails.AppendText("", true);
						break;
					}
					case eInSetResult_t.eDuplicateSetting:
					case eInSetResult_t.eDuplicateDelete:
					{
						string text2 = policyItemCollection_t.FormattedData(policyItemInContext_t.representativeData).Replace("\r\n", "\v");
						this.rtDetails.AppendBoldItalicAndNormalText("Option:\t", text2, true);
						if (text2 != policyItemInContext_t.DisplayText)
						{
							this.rtDetails.AppendBoldItalicAndNormalText("Data:\t", policyItemInContext_t.DisplayText, true);
						}
						if (policyItemInContext_t.representativeData.SettingType.Length > 0)
						{
							this.rtDetails.AppendBoldItalicAndNormalText("Type:\t", policyItemInContext_t.representativeData.SettingType, true);
						}
						if (this.m_bShowGPOsInDetails)
						{
							this.rtDetails.AppendBoldText("Defined in the following GPOs:", true);
							foreach (PolicyRules.PolicySettingData_t policySettingData_t in sortedList.Values)
							{
								this.rtDetails.AppendBoldItalicAndNormalText("GPO:\t", policySettingData_t.GpoName, true);
								if (this.m_bShowGPOsAndFilesInDetails)
								{
									this.rtDetails.AppendBoldItalicAndNormalText("File:\t", policySettingData_t.SourceFile, true);
								}
							}
						}
						this.rtDetails.AppendText("", true);
						break;
					}
					case eInSetResult_t.eConflictingSettings:
						foreach (PolicyRules.PolicySettingData_t policySettingData_t2 in sortedList.Values)
						{
							string text3 = policyItemCollection_t.FormattedData(policySettingData_t2).Replace("\r\n", "\v");
							this.rtDetails.AppendBoldItalicAndNormalText("Option:\t", text3, true);
							if (text3 != policySettingData_t2.SettingData)
							{
								this.rtDetails.AppendBoldItalicAndNormalText("Data:\t", policySettingData_t2.SettingData, true);
							}
							if (policySettingData_t2.SettingType.Length > 0)
							{
								this.rtDetails.AppendBoldItalicAndNormalText("Type:\t", policySettingData_t2.SettingType, true);
							}
							if (this.m_bShowGPOsInDetails)
							{
								this.rtDetails.AppendBoldItalicAndNormalText("GPO:\t", policySettingData_t2.GpoName, true);
								if (this.m_bShowGPOsAndFilesInDetails)
								{
									this.rtDetails.AppendBoldItalicAndNormalText("File:\t", policySettingData_t2.SourceFile, true);
								}
							}
							this.rtDetails.AppendText("", true);
						}
						break;
					}
				}
			}
			this.rtDetails.GoToTop();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00016718 File Offset: 0x00014918
		private void showDetailsPaneToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.showDetailsPaneToolStripMenuItem1.Checked = (this.m_ShowDetailsPane = !this.m_ShowDetailsPane);
			Globals.bLastShowDetailsPane = this.m_ShowDetailsPane;
			Globals.SaveConfig();
			this.ShowHideDetailsPane();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00016758 File Offset: 0x00014958
		private void ShowHideDetailsPane()
		{
			if (!this.m_ShowDetailsPane)
			{
				this.splitContainer1.Panel2.Hide();
				this.splitContainer1.SplitterWidth = 1;
				this.splitContainer1.SplitterDistance = base.Height;
				this.splitContainer1.BorderStyle = BorderStyle.None;
				this.splitContainer1.IsSplitterFixed = true;
				return;
			}
			this.splitContainer1.Panel2.Show();
			this.splitContainer1.SplitterDistance = 6;
			this.splitContainer1.SplitterDistance = base.Height / 2;
			this.splitContainer1.BorderStyle = BorderStyle.Fixed3D;
			this.splitContainer1.IsSplitterFixed = false;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000167FC File Offset: 0x000149FC
		private void showGPONamesAndFilesInDetailsPaneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.m_bShowGPOsAndFilesInDetails = !this.m_bShowGPOsAndFilesInDetails;
			this.showGPONamesAndFilesInDetailsPaneToolStripMenuItem.Checked = (Globals.bLastShowGPOsAndFilesInDetails = this.m_bShowGPOsAndFilesInDetails);
			if (this.m_bShowGPOsAndFilesInDetails && !this.m_bShowGPOsInDetails)
			{
				this.showGPONamesInDetailsPaneToolStripMenuItem.Checked = (Globals.bLastShowGPOsInDetails = (this.m_bShowGPOsInDetails = true));
			}
			Globals.SaveConfig();
			this.RefreshDetailsPane();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00016868 File Offset: 0x00014A68
		private void showGPONamesInDetailsPaneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.m_bShowGPOsInDetails = !this.m_bShowGPOsInDetails;
			this.showGPONamesInDetailsPaneToolStripMenuItem.Checked = (Globals.bLastShowGPOsInDetails = this.m_bShowGPOsInDetails);
			if (!this.m_bShowGPOsInDetails && this.m_bShowGPOsAndFilesInDetails)
			{
				this.showGPONamesAndFilesInDetailsPaneToolStripMenuItem.Checked = (Globals.bLastShowGPOsAndFilesInDetails = (this.m_bShowGPOsAndFilesInDetails = false));
			}
			Globals.SaveConfig();
			this.RefreshDetailsPane();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000168D4 File Offset: 0x00014AD4
		private void showExplanationTextForSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.showExplanationTextForSettingsToolStripMenuItem.Checked = (this.m_bShowExplainText = !this.m_bShowExplainText);
			Globals.bLastShowExplainText = this.m_bShowExplainText;
			Globals.SaveConfig();
			this.RefreshDetailsPane();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00003891 File Offset: 0x00001A91
		private void SetViewCheckmarks()
		{
			this.showOnlyConflictToolStripMenuItem.Checked = this.m_show == PolicyViewer3.eShowCompare_t.esc_ShowOnlyConflicts;
			this.showOnlyDifferencesToolStripMenuItem.Checked = this.m_show == PolicyViewer3.eShowCompare_t.esc_ShowOnlyDifferences;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00016914 File Offset: 0x00014B14
		private void showOnlyDifferencesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (this.m_show)
			{
			case PolicyViewer3.eShowCompare_t.esc_ShowAll:
			case PolicyViewer3.eShowCompare_t.esc_ShowOnlyConflicts:
				this.m_show = PolicyViewer3.eShowCompare_t.esc_ShowOnlyDifferences;
				break;
			case PolicyViewer3.eShowCompare_t.esc_ShowOnlyDifferences:
				this.m_show = PolicyViewer3.eShowCompare_t.esc_ShowAll;
				break;
			}
			this.SetViewCheckmarks();
			this.Redisplay();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00016958 File Offset: 0x00014B58
		private void showOnlyConflictToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PolicyViewer3.eShowCompare_t show = this.m_show;
			if (show > PolicyViewer3.eShowCompare_t.esc_ShowOnlyDifferences)
			{
				if (show == PolicyViewer3.eShowCompare_t.esc_ShowOnlyConflicts)
				{
					this.m_show = PolicyViewer3.eShowCompare_t.esc_ShowAll;
				}
			}
			else
			{
				this.m_show = PolicyViewer3.eShowCompare_t.esc_ShowOnlyConflicts;
			}
			this.SetViewCheckmarks();
			this.Redisplay();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000038BB File Offset: 0x00001ABB
		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.rtDetails.Focused || this.rtDetails.RTBox.Focused)
			{
				this.rtDetails.RTSelectAll(sender, e);
				return;
			}
			this.dataGridView1.SelectAll();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00016990 File Offset: 0x00014B90
		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.rtDetails.Focused || this.rtDetails.RTBox.Focused)
			{
				this.rtDetails.RTCopy(sender, e);
				return;
			}
			if (this.dataGridView1.SelectedRows.Count > 0)
			{
				Clipboard.SetDataObject(this.dataGridView1.GetClipboardContent());
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000169F0 File Offset: 0x00014BF0
		private void findToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.RowCount == 0)
			{
				return;
			}
			FindDlg findDlg = new FindDlg();
			if (DialogResult.OK != findDlg.ShowDialog())
			{
				return;
			}
			this.m_searchText = findDlg.m_sFindText.ToLower();
			this.FindText();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000038F5 File Offset: 0x00001AF5
		private void findAgainToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.RowCount == 0)
			{
				return;
			}
			this.FindText();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00016A34 File Offset: 0x00014C34
		private void FindText()
		{
			if (this.m_searchText.Length == 0)
			{
				return;
			}
			int i = 0;
			if (this.dataGridView1.CurrentRow != null)
			{
				i = this.dataGridView1.CurrentRow.Index + 1;
			}
			while (i < this.dataGridView1.RowCount)
			{
				DataGridViewRow dataGridViewRow = this.dataGridView1.Rows[i];
				foreach (object obj in dataGridViewRow.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					if (dataGridViewCell.Value.ToString().ToLower().Contains(this.m_searchText))
					{
						dataGridViewRow.Selected = true;
						this.dataGridView1.CurrentCell = dataGridViewCell;
						this.dataGridView1.Focus();
						return;
					}
				}
				foreach (PolicyPath_t policyPath_t in ((PolicyItemCollection_t)dataGridViewRow.Tag).m_polPaths)
				{
					if (policyPath_t.m_sPolConfig.ToLower().Contains(this.m_searchText) || policyPath_t.m_sPolPath.ToLower().Contains(this.m_searchText) || policyPath_t.m_sPolName.ToLower().Contains(this.m_searchText))
					{
						dataGridViewRow.Selected = true;
						this.dataGridView1.CurrentCell = dataGridViewRow.Cells[0];
						this.dataGridView1.Focus();
						return;
					}
				}
				i++;
			}
			MessageBox.Show("Cannot find \"" + this.m_searchText + "\"", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00016C14 File Offset: 0x00014E14
		private void ExportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProcessRunning processRunning = new ProcessRunning(this.Text, "Exporting table to Microsoft Excel...", 0, this.dataGridView1.Rows.Count);
			processRunning.Show(this);
			processRunning.Update();
			ExcelWriter.WriteDataGridViewToExcel(this.dataGridView1, new ExcelWriter.ProgressIndicator(ProcessRunning.Progress));
			processRunning.Dispose();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00016C6C File Offset: 0x00014E6C
		private void exportAllDataToExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProcessRunning processRunning = new ProcessRunning(this.Text, "Exporting all data to Microsoft Excel...", 0, this.dataGridView1.Rows.Count);
			processRunning.Show(this);
			processRunning.Update();
			int num;
			if (this.m_bShowGPOsAndFilesInDetails)
			{
				num = this.m_policyCollection.CollectionCount() * 5;
			}
			else if (this.m_bShowGPOsInDetails)
			{
				num = this.m_policyCollection.CollectionCount() * 4;
			}
			else
			{
				num = this.m_policyCollection.CollectionCount() * 3;
			}
			num += 6;
			if (this.m_bShowExplainText)
			{
				num++;
			}
			int rowCount = this.dataGridView1.RowCount;
			ProcessRunning.Progress(0, rowCount, 0);
			ExcelWriter.Doc_t doc_t = new ExcelWriter.Doc_t(num);
			doc_t.m_colHeaders[0] = "Policy Config";
			doc_t.m_colHeaders[1] = "Policy Path";
			doc_t.m_colHeaders[2] = "Policy Setting Name";
			doc_t.m_colHeaders[3] = "Policy Type";
			doc_t.m_colHeaders[4] = "Policy Group or Registry Key";
			doc_t.m_colHeaders[5] = "Policy Setting";
			for (int i = 0; i < this.m_policyCollection.m_nameAndPolicyRules.Length; i++)
			{
				doc_t.m_colHeaders[6 + i * 3] = this.m_policyCollection.m_nameAndPolicyRules[i].m_sName;
				doc_t.m_colHeaders[7 + i * 3] = this.m_policyCollection.m_nameAndPolicyRules[i].m_sName + "\r\nOption";
				doc_t.m_colHeaders[8 + i * 3] = this.m_policyCollection.m_nameAndPolicyRules[i].m_sName + "\r\nType";
			}
			int num2 = 9 + (this.m_policyCollection.m_nameAndPolicyRules.Length - 1) * 3;
			int num3 = -1;
			int num4 = -1;
			int num5 = -1;
			if (this.m_bShowExplainText)
			{
				num3 = num2++;
				doc_t.m_colHeaders[num3] = "Explain Text";
			}
			if (this.m_bShowGPOsInDetails)
			{
				num5 = (this.m_bShowGPOsAndFilesInDetails ? 2 : 1);
				num4 = num2;
				for (int j = 0; j < this.m_policyCollection.m_nameAndPolicyRules.Length; j++)
				{
					doc_t.m_colHeaders[num4 + j * num5] = this.m_policyCollection.m_nameAndPolicyRules[j].m_sName + "\r\nGPO";
					if (this.m_bShowGPOsAndFilesInDetails)
					{
						doc_t.m_colHeaders[num4 + 1 + j * num5] = this.m_policyCollection.m_nameAndPolicyRules[j].m_sName + "\r\nGPO file";
					}
				}
			}
			int num6 = 0;
			foreach (object obj in ((IEnumerable)this.dataGridView1.Rows))
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				ProcessRunning.Progress(0, rowCount, num6++);
				ExcelWriter.Row_t row_t = doc_t.AddRow();
				PolicyItemCollection_t policyItemCollection_t = (PolicyItemCollection_t)dataGridViewRow.Tag;
				foreach (PolicyPath_t policyPath_t in policyItemCollection_t.m_polPaths)
				{
					row_t.m_cells[0].AddText(policyPath_t.m_sPolConfig);
					row_t.m_cells[1].AddText(policyPath_t.m_sPolPath);
					if (policyPath_t.m_sPolNameSubOption.Length > 0)
					{
						row_t.m_cells[2].AddText(policyPath_t.m_sPolName + " --> " + policyPath_t.m_sPolNameSubOption);
					}
					else
					{
						row_t.m_cells[2].AddText(policyPath_t.m_sPolName);
					}
					if (this.m_bShowExplainText)
					{
						row_t.m_cells[num3].AddText(policyPath_t.ExplainTextFormatted());
					}
				}
				row_t.m_cells[3].AddText(dataGridViewRow.Cells[0].Value.ToString());
				row_t.m_cells[4].AddText(dataGridViewRow.Cells[1].Value.ToString());
				row_t.m_cells[5].AddText(dataGridViewRow.Cells[2].Value.ToString());
				for (int k = 0; k < this.m_policyCollection.m_nameAndPolicyRules.Length; k++)
				{
					int num7 = 6 + k * 3;
					int num8 = 7 + k * 3;
					int num9 = 8 + k * 3;
					int num10 = num4 + k * num5;
					int num11 = num4 + 1 + k * num5;
					PolicyItemInContext_t policyItemInContext_t = policyItemCollection_t.PolicyItemInContext(k);
					Color color = Color.Empty;
					eInSetResult_t inSetResult = policyItemInContext_t.InSetResult;
					if (inSetResult != eInSetResult_t.eNoSetting)
					{
						if (inSetResult != eInSetResult_t.eDuplicateSetting)
						{
							if (inSetResult == eInSetResult_t.eConflictingSettings)
							{
								color = Color.Yellow;
							}
						}
						else
						{
							color = Color.FromArgb(240, 240, 240);
						}
					}
					else
					{
						color = Color.DarkGray;
					}
					if (!color.IsEmpty)
					{
						row_t.m_cells[num7].m_color = (row_t.m_cells[num8].m_color = color);
					}
					if (policyItemCollection_t.m_settingData[k] != null)
					{
						switch (policyItemInContext_t.InItemResult)
						{
						case eInSetResult_t.eNoSetting:
							goto IL_071F;
						case eInSetResult_t.eSingleSetting:
						case eInSetResult_t.eSingleDelete:
						{
							string text = policyItemCollection_t.FormattedData(policyItemInContext_t.representativeData);
							row_t.m_cells[num7].AddText(policyItemInContext_t.representativeData.SettingData);
							row_t.m_cells[num8].AddText(text);
							row_t.m_cells[num9].AddText(policyItemInContext_t.representativeData.SettingType);
							if (!this.m_bShowGPOsInDetails)
							{
								goto IL_071F;
							}
							row_t.m_cells[num10].AddText(policyItemInContext_t.representativeData.GpoName);
							if (this.m_bShowGPOsAndFilesInDetails)
							{
								row_t.m_cells[num11].AddText(policyItemInContext_t.representativeData.SourceFile);
								goto IL_071F;
							}
							goto IL_071F;
						}
						case eInSetResult_t.eDuplicateSetting:
						case eInSetResult_t.eDuplicateDelete:
						{
							string text2 = policyItemCollection_t.FormattedData(policyItemInContext_t.representativeData);
							row_t.m_cells[num7].AddText(policyItemInContext_t.representativeData.SettingData);
							row_t.m_cells[num8].AddText(text2);
							row_t.m_cells[num9].AddText(policyItemInContext_t.representativeData.SettingType);
							if (!this.m_bShowGPOsInDetails)
							{
								goto IL_071F;
							}
							using (IEnumerator<PolicyRules.PolicySettingData_t> enumerator3 = policyItemCollection_t.m_settingData[k].Values.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									PolicyRules.PolicySettingData_t policySettingData_t = enumerator3.Current;
									row_t.m_cells[num10].AddText(policySettingData_t.GpoName);
									if (this.m_bShowGPOsAndFilesInDetails)
									{
										row_t.m_cells[num11].AddText(policySettingData_t.SourceFile);
									}
								}
								goto IL_071F;
							}
							break;
						}
						case eInSetResult_t.eConflictingSettings:
							break;
						default:
							goto IL_071F;
						}
						foreach (PolicyRules.PolicySettingData_t policySettingData_t2 in policyItemCollection_t.m_settingData[k].Values)
						{
							string text3 = policyItemCollection_t.FormattedData(policySettingData_t2);
							row_t.m_cells[num7].AddText(policySettingData_t2.SettingData);
							row_t.m_cells[num8].AddText(text3);
							row_t.m_cells[num9].AddText(policySettingData_t2.SettingType);
							if (this.m_bShowGPOsInDetails)
							{
								row_t.m_cells[num10].AddText(policySettingData_t2.GpoName);
								if (this.m_bShowGPOsAndFilesInDetails)
								{
									row_t.m_cells[num11].AddText(policySettingData_t2.SourceFile);
								}
							}
						}
					}
					IL_071F:;
				}
			}
			ProcessRunning.SetProgressText("Writing all data into Excel worksheet...");
			ProcessRunning.Progress(0, 1, 0);
			ExcelWriter.WriteDocToExcel(doc_t, new ExcelWriter.ProgressIndicator(ProcessRunning.Progress));
			processRunning.Dispose();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00017464 File Offset: 0x00015664
		private void gPOFilterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GpoPicker3 gpoPicker = new GpoPicker3(this.m_policyCollection.m_nameAndPolicyRules);
			if (DialogResult.OK == gpoPicker.ShowDialog(this))
			{
				foreach (NameAndPolicyRules_t nameAndPolicyRules_t in this.m_policyCollection.m_nameAndPolicyRules)
				{
					string text;
					if (nameAndPolicyRules_t.m_rules != null && !nameAndPolicyRules_t.m_rules.Reload(out text))
					{
						MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					this.m_policyCollection.ReloadData();
					this.Redisplay();
				}
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000390B File Offset: 0x00001B0B
		private void clientSideExtensionsCSEsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new CseViewer(this.m_policyCollection.m_nameAndPolicyRules).ShowDialog(this);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00018570 File Offset: 0x00016770
		private void exportAllDataToCSVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1 == null || this.dataGridView1.Rows.Count == 0)
			{
				MessageBox.Show("No data loaded to export.", "Nothing to Export", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
				saveFileDialog.FileName = "PolicyAnalyzer_AllData_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv";
				saveFileDialog.Title = "Export All Data to CSV";
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					ProcessRunning processRunning = new ProcessRunning(this.Text, "Exporting all data to CSV...", 0, this.dataGridView1.Rows.Count);
					processRunning.Show(this);
					processRunning.Update();
					try
					{
						using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false, new UTF8Encoding(true)))
						{
							List<string> list = new List<string> { "Policy Config", "Policy Path", "Policy Setting Name", "Policy Type", "Policy Group or Registry Key", "Policy Setting" };
							for (int i = 0; i < this.m_policyCollection.m_nameAndPolicyRules.Length; i++)
							{
								string sName = this.m_policyCollection.m_nameAndPolicyRules[i].m_sName;
								list.Add(sName);
								list.Add(sName + " Option");
								list.Add(sName + " Type");
							}
							if (this.m_bShowExplainText)
							{
								list.Add("Explain Text");
							}
							if (this.m_bShowGPOsInDetails)
							{
								for (int j = 0; j < this.m_policyCollection.m_nameAndPolicyRules.Length; j++)
								{
									string sName2 = this.m_policyCollection.m_nameAndPolicyRules[j].m_sName;
									list.Add(sName2 + " GPO");
									if (this.m_bShowGPOsAndFilesInDetails)
									{
										list.Add(sName2 + " GPO file");
									}
								}
							}
							streamWriter.WriteLine(string.Join(",", Enumerable.Select<string, string>(list, (string h, int index) => this.CsvQuote(h, index))));
							List<ValueTuple<DataGridViewRow, PolicyItemCollection_t, string, string, string>> list2 = new List<ValueTuple<DataGridViewRow, PolicyItemCollection_t, string, string, string>>();
							int num = 0;
							int count = this.dataGridView1.Rows.Count;
							foreach (object obj in ((IEnumerable)this.dataGridView1.Rows))
							{
								DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
								if (!dataGridViewRow.IsNewRow)
								{
									num++;
									ProcessRunning.Progress(0, count, num);
									PolicyItemCollection_t policyItemCollection_t = (PolicyItemCollection_t)dataGridViewRow.Tag;
									if (policyItemCollection_t != null)
									{
										string text = "";
										string text2 = "";
										string text3 = "";
										if (policyItemCollection_t.m_polPaths.Count > 0)
										{
											PolicyPath_t policyPath_t = policyItemCollection_t.m_polPaths[0];
											text = policyPath_t.m_sPolConfig ?? "";
											text2 = policyPath_t.m_sPolPath ?? "";
											text3 = policyPath_t.m_sPolName ?? "";
											if (!string.IsNullOrEmpty(policyPath_t.m_sPolNameSubOption))
											{
												text3 = text3 + " --> " + policyPath_t.m_sPolNameSubOption;
											}
										}
										list2.Add(new ValueTuple<DataGridViewRow, PolicyItemCollection_t, string, string, string>(dataGridViewRow, policyItemCollection_t, text, text2, text3));
									}
								}
							}
							list2.Sort(delegate([TupleElementNames(new string[] { "dgvRow", "pic", "config", "path", "name" })] ValueTuple<DataGridViewRow, PolicyItemCollection_t, string, string, string> a, [TupleElementNames(new string[] { "dgvRow", "pic", "config", "path", "name" })] ValueTuple<DataGridViewRow, PolicyItemCollection_t, string, string, string> b)
							{
								int num3 = string.Compare(a.Item3, b.Item3, StringComparison.OrdinalIgnoreCase);
								if (num3 != 0)
								{
									return num3;
								}
								num3 = string.Compare(a.Item4, b.Item4, StringComparison.OrdinalIgnoreCase);
								if (num3 != 0)
								{
									return num3;
								}
								num3 = string.Compare(a.Item5, b.Item5, StringComparison.OrdinalIgnoreCase);
								if (num3 != 0)
								{
									return num3;
								}
								DataGridViewCell dataGridViewCell4 = a.Item1.Cells[0];
								string text18;
								if (dataGridViewCell4 == null)
								{
									text18 = null;
								}
								else
								{
									object value4 = dataGridViewCell4.Value;
									text18 = ((value4 != null) ? value4.ToString() : null);
								}
								string text19 = text18 ?? "";
								DataGridViewCell dataGridViewCell5 = b.Item1.Cells[0];
								string text20;
								if (dataGridViewCell5 == null)
								{
									text20 = null;
								}
								else
								{
									object value5 = dataGridViewCell5.Value;
									text20 = ((value5 != null) ? value5.ToString() : null);
								}
								num3 = string.Compare(text19, text20 ?? "", StringComparison.OrdinalIgnoreCase);
								if (num3 != 0)
								{
									return num3;
								}
								DataGridViewCell dataGridViewCell6 = a.Item1.Cells[1];
								string text21;
								if (dataGridViewCell6 == null)
								{
									text21 = null;
								}
								else
								{
									object value6 = dataGridViewCell6.Value;
									text21 = ((value6 != null) ? value6.ToString() : null);
								}
								string text22 = text21 ?? "";
								DataGridViewCell dataGridViewCell7 = b.Item1.Cells[1];
								string text23;
								if (dataGridViewCell7 == null)
								{
									text23 = null;
								}
								else
								{
									object value7 = dataGridViewCell7.Value;
									text23 = ((value7 != null) ? value7.ToString() : null);
								}
								num3 = string.Compare(text22, text23 ?? "", StringComparison.OrdinalIgnoreCase);
								if (num3 != 0)
								{
									return num3;
								}
								DataGridViewCell dataGridViewCell8 = a.Item1.Cells[2];
								string text24;
								if (dataGridViewCell8 == null)
								{
									text24 = null;
								}
								else
								{
									object value8 = dataGridViewCell8.Value;
									text24 = ((value8 != null) ? value8.ToString() : null);
								}
								string text25 = text24 ?? "";
								DataGridViewCell dataGridViewCell9 = b.Item1.Cells[2];
								string text26;
								if (dataGridViewCell9 == null)
								{
									text26 = null;
								}
								else
								{
									object value9 = dataGridViewCell9.Value;
									text26 = ((value9 != null) ? value9.ToString() : null);
								}
								return string.Compare(text25, text26 ?? "", StringComparison.OrdinalIgnoreCase);
							});
							int num2 = 0;
							foreach (ValueTuple<DataGridViewRow, PolicyItemCollection_t, string, string, string> valueTuple in list2)
							{
								DataGridViewRow item = valueTuple.Item1;
								PolicyItemCollection_t item2 = valueTuple.Item2;
								num2++;
								ProcessRunning.Progress(0, count, num2);
								List<string> list3 = new List<string>();
								string item3 = valueTuple.Item3;
								string item4 = valueTuple.Item4;
								string item5 = valueTuple.Item5;
								string text4 = "";
								if (this.m_bShowExplainText && item2.m_polPaths.Count > 0)
								{
									text4 = item2.m_polPaths[item2.m_polPaths.Count - 1].ExplainTextFormatted() ?? "";
								}
								list3.Add(item3);
								list3.Add(item4);
								list3.Add(item5);
								List<string> list4 = list3;
								DataGridViewCell dataGridViewCell = item.Cells[0];
								string text5;
								if (dataGridViewCell == null)
								{
									text5 = null;
								}
								else
								{
									object value = dataGridViewCell.Value;
									text5 = ((value != null) ? value.ToString() : null);
								}
								list4.Add(text5 ?? "");
								List<string> list5 = list3;
								DataGridViewCell dataGridViewCell2 = item.Cells[1];
								string text6;
								if (dataGridViewCell2 == null)
								{
									text6 = null;
								}
								else
								{
									object value2 = dataGridViewCell2.Value;
									text6 = ((value2 != null) ? value2.ToString() : null);
								}
								list5.Add(text6 ?? "");
								List<string> list6 = list3;
								DataGridViewCell dataGridViewCell3 = item.Cells[2];
								string text7;
								if (dataGridViewCell3 == null)
								{
									text7 = null;
								}
								else
								{
									object value3 = dataGridViewCell3.Value;
									text7 = ((value3 != null) ? value3.ToString() : null);
								}
								list6.Add(text7 ?? "");
								for (int k = 0; k < this.m_policyCollection.m_nameAndPolicyRules.Length; k++)
								{
									item2.PolicyItemInContext(k);
									StringBuilder stringBuilder = new StringBuilder();
									StringBuilder stringBuilder2 = new StringBuilder();
									StringBuilder stringBuilder3 = new StringBuilder();
									string text8 = null;
									string text9 = null;
									string text10 = null;
									if (item2.m_settingData != null && item2.m_settingData[k] != null)
									{
										foreach (PolicyRules.PolicySettingData_t policySettingData_t in item2.m_settingData[k].Values)
										{
											string text11 = (policySettingData_t.SettingData ?? "").Trim(new char[] { '\r', '\n' });
											string text12 = (item2.FormattedData(policySettingData_t) ?? "").Trim(new char[] { '\r', '\n' });
											string text13 = (policySettingData_t.SettingType ?? "").Trim(new char[] { '\r', '\n' });
											if (text8 != null && text11 != text8)
											{
												if (stringBuilder.Length > 0)
												{
													stringBuilder.Append("\r\n");
												}
												stringBuilder.Append(text11);
											}
											else if (text8 == null)
											{
												stringBuilder.Append(text11);
											}
											if (text11.Length > 0)
											{
												text8 = text11;
											}
											if (text9 != null && text12 != text9)
											{
												if (stringBuilder2.Length > 0)
												{
													stringBuilder2.Append("\r\n");
												}
												stringBuilder2.Append(text12);
											}
											else if (text9 == null)
											{
												stringBuilder2.Append(text12);
											}
											if (text12.Length > 0)
											{
												text9 = text12;
											}
											if (text10 != null && text13 != text10)
											{
												if (stringBuilder3.Length > 0)
												{
													stringBuilder3.Append("\r\n");
												}
												stringBuilder3.Append(text13);
											}
											else if (text10 == null)
											{
												stringBuilder3.Append(text13);
											}
											if (text13.Length > 0)
											{
												text10 = text13;
											}
										}
									}
									list3.Add(stringBuilder.ToString());
									list3.Add(stringBuilder2.ToString());
									list3.Add(stringBuilder3.ToString());
								}
								if (this.m_bShowExplainText)
								{
									list3.Add(text4);
								}
								if (this.m_bShowGPOsInDetails)
								{
									for (int l = 0; l < this.m_policyCollection.m_nameAndPolicyRules.Length; l++)
									{
										item2.PolicyItemInContext(l);
										StringBuilder stringBuilder4 = new StringBuilder();
										StringBuilder stringBuilder5 = new StringBuilder();
										string text14 = null;
										string text15 = null;
										if (item2.m_settingData != null && item2.m_settingData[l] != null)
										{
											foreach (PolicyRules.PolicySettingData_t policySettingData_t2 in item2.m_settingData[l].Values)
											{
												string text16 = (policySettingData_t2.GpoName ?? "").Trim(new char[] { '\r', '\n' });
												string text17 = (policySettingData_t2.SourceFile ?? "").Trim(new char[] { '\r', '\n' });
												if (text14 != null && text16 != text14)
												{
													if (stringBuilder4.Length > 0)
													{
														stringBuilder4.Append("\r\n");
													}
													stringBuilder4.Append(text16);
												}
												else if (text14 == null)
												{
													stringBuilder4.Append(text16);
												}
												if (text16.Length > 0)
												{
													text14 = text16;
												}
												if (text15 != null && text17 != text15)
												{
													if (stringBuilder5.Length > 0)
													{
														stringBuilder5.Append("\r\n");
													}
													stringBuilder5.Append(text17);
												}
												else if (text15 == null)
												{
													stringBuilder5.Append(text17);
												}
												if (text17.Length > 0)
												{
													text15 = text17;
												}
											}
										}
										list3.Add(stringBuilder4.ToString());
										if (this.m_bShowGPOsAndFilesInDetails)
										{
											list3.Add(stringBuilder5.ToString());
										}
									}
								}
								streamWriter.WriteLine(string.Join(",", Enumerable.Select<string, string>(list3, (string v, int index) => this.CsvQuote(v, index))));
							}
							ProcessRunning.SetProgressText("CSV export complete");
						}
						MessageBox.Show("CSV file created successfully:\r\n" + saveFileDialog.FileName, "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Export failed:\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					finally
					{
						processRunning.Dispose();
					}
				}
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00018FA4 File Offset: 0x000171A4
		private string CsvQuote(string field, int index)
		{
			if (string.IsNullOrEmpty(field))
			{
				return "";
			}
			string text = field.Replace("\"", "\"\"");
			if (index == 6 && (field == "CISADMIN" || field == "CISGUEST"))
			{
				return "\"\"\"" + text + "\"\"\"";
			}
			if (text.Contains(",") || text.Contains("\r") || text.Contains("\n") || text.Contains("\""))
			{
				return "\"" + text + "\"";
			}
			return text;
		}

		// Token: 0x040007A7 RID: 1959
		private const string csPolicyType = "Policy Type";

		// Token: 0x040007A8 RID: 1960
		private const string csPolicyGroupOrRegKey = "Policy Group or Registry Key";

		// Token: 0x040007A9 RID: 1961
		private const string csPolicySetting = "Policy Setting";

		// Token: 0x040007AA RID: 1962
		private int m_compareCount;

		// Token: 0x040007AB RID: 1963
		private PolicyViewer3.eShowCompare_t m_show;

		// Token: 0x040007AC RID: 1964
		private bool m_ShowDetailsPane = Globals.bLastShowDetailsPane;

		// Token: 0x040007AD RID: 1965
		private bool m_bShowGPOsInDetails = Globals.bLastShowGPOsInDetails;

		// Token: 0x040007AE RID: 1966
		private bool m_bShowGPOsAndFilesInDetails = Globals.bLastShowGPOsAndFilesInDetails;

		// Token: 0x040007AF RID: 1967
		private bool m_bShowExplainText = Globals.bLastShowExplainText;

		// Token: 0x040007B0 RID: 1968
		private string m_sBaseTitle = string.Empty;

		// Token: 0x040007B1 RID: 1969
		private string m_searchText = string.Empty;

		// Token: 0x040007B2 RID: 1970
		private PolicyCollection m_policyCollection = new PolicyCollection();

		// Token: 0x040007B3 RID: 1971
		private const int cIxDataOffset = 3;

		// Token: 0x0200007A RID: 122
		private enum eShowCompare_t
		{
			// Token: 0x040007D4 RID: 2004
			esc_ShowAll,
			// Token: 0x040007D5 RID: 2005
			esc_ShowOnlyDifferences,
			// Token: 0x040007D6 RID: 2006
			esc_ShowOnlyConflicts
		}
	}
}
