using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PolicyAnalyzer
{
	// Token: 0x02000075 RID: 117
	public partial class GpoPicker3 : Form
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00012400 File Offset: 0x00010600
		public GpoPicker3(NameAndPolicyRules_t[] nameAndPolicyRules)
		{
			this.m_nameAndPolicyRules = nameAndPolicyRules;
			this.InitializeComponent();
			this.m_checkedListBoxes = new CheckedListBox[this.m_nameAndPolicyRules.Length];
			for (int i = 0; i < this.m_nameAndPolicyRules.Length; i++)
			{
				CheckedListBox checkedListBox = (this.m_checkedListBoxes[i] = new CheckedListBox());
				this.ChkboxFmt(checkedListBox, i);
				if (nameAndPolicyRules[i].m_rules != null)
				{
					SortedList<string, bool> gpoList = nameAndPolicyRules[i].m_rules.m_GpoList;
					foreach (string text in gpoList.Keys)
					{
						int num = checkedListBox.Items.Add(text);
						checkedListBox.SetItemChecked(num, gpoList[text]);
					}
				}
			}
			foreach (NameAndPolicyRules_t nameAndPolicyRules_t in nameAndPolicyRules)
			{
				this.cmbGpoSetNames.Items.Add(nameAndPolicyRules_t.m_sName);
			}
			this.cmbGpoSetNames.SelectedIndex = 0;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00012530 File Offset: 0x00010730
		private void ChkboxFmt(CheckedListBox chkbox, int ixPol)
		{
			chkbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			chkbox.CheckOnClick = true;
			chkbox.FormattingEnabled = true;
			chkbox.Margin = new Padding(4);
			chkbox.Name = "checkedListBox" + ixPol.ToString();
			int num = 4;
			int x = this.cmbGpoSetNames.Location.X;
			int num2 = this.chkSelectAll.Location.Y + this.chkSelectAll.Size.Height + num;
			int width = this.cmbGpoSetNames.Size.Width;
			int num3 = this.btnOK.Location.Y - num2 - num;
			chkbox.Location = new Point(x, num2);
			chkbox.Size = new Size(width, num3);
			chkbox.TabIndex = 10;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0001260C File Offset: 0x0001080C
		private void cmbGpoSetNames_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = this.cmbGpoSetNames.SelectedIndex;
			NameAndPolicyRules_t[] nameAndPolicyRules = this.m_nameAndPolicyRules;
			if (this.m_curr >= 0)
			{
				base.Controls.Remove(this.m_checkedListBoxes[this.m_curr]);
				this.m_curr = -1;
			}
			CheckedListBox checkedListBox = this.m_checkedListBoxes[selectedIndex];
			base.Controls.Add(checkedListBox);
			this.lblPolCount.Text = "GPO count: " + checkedListBox.Items.Count.ToString();
			bool flag = true;
			bool flag2 = false;
			for (int i = 0; i < checkedListBox.Items.Count; i++)
			{
				if (checkedListBox.GetItemChecked(i))
				{
					flag2 = true;
				}
				else
				{
					flag = false;
				}
			}
			if (flag)
			{
				this.chkSelectAll.CheckState = CheckState.Checked;
			}
			else if (flag2)
			{
				this.chkSelectAll.CheckState = CheckState.Indeterminate;
			}
			else
			{
				this.chkSelectAll.CheckState = CheckState.Unchecked;
			}
			this.m_curr = selectedIndex;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000126FC File Offset: 0x000108FC
		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < this.m_nameAndPolicyRules.Length; i++)
				{
					if (this.m_nameAndPolicyRules[i].m_rules != null)
					{
						SortedList<string, bool> gpoList = this.m_nameAndPolicyRules[i].m_rules.m_GpoList;
						CheckedListBox checkedListBox = this.m_checkedListBoxes[i];
						for (int j = 0; j < checkedListBox.Items.Count; j++)
						{
							string text = checkedListBox.Items[j].ToString();
							bool itemChecked = checkedListBox.GetItemChecked(j);
							gpoList[text] = itemChecked;
						}
					}
				}
				base.DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Unexpected error: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			base.Close();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000035D1 File Offset: 0x000017D1
		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000127D0 File Offset: 0x000109D0
		private void btnCopyList_Click(object sender, EventArgs e)
		{
			if (this.m_curr < 0)
			{
				return;
			}
			CheckedListBox checkedListBox = this.m_checkedListBoxes[this.m_curr];
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this.m_nameAndPolicyRules[this.m_curr].m_rules.m_GpoList.Keys)
			{
				stringBuilder.Append(text + "\r\n");
			}
			Clipboard.SetText(stringBuilder.ToString());
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0001286C File Offset: 0x00010A6C
		private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.chkSelectAll.Checked;
			if (this.m_curr >= 0)
			{
				CheckedListBox checkedListBox = this.m_checkedListBoxes[this.m_curr];
				for (int i = 0; i < checkedListBox.Items.Count; i++)
				{
					checkedListBox.SetItemChecked(i, @checked);
				}
			}
		}

		// Token: 0x04000768 RID: 1896
		private NameAndPolicyRules_t[] m_nameAndPolicyRules;

		// Token: 0x04000769 RID: 1897
		private CheckedListBox[] m_checkedListBoxes;

		// Token: 0x0400076A RID: 1898
		private int m_curr = -1;
	}
}
