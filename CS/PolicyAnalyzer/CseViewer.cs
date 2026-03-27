using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PolicyAnalyzer
{
	// Token: 0x02000066 RID: 102
	public partial class CseViewer : Form
	{
		// Token: 0x06000177 RID: 375 RVA: 0x000102F4 File Offset: 0x0000E4F4
		public CseViewer(NameAndPolicyRules_t[] nameAndPolicyRules)
		{
			this.m_nameAndPolicyRules = nameAndPolicyRules;
			this.InitializeComponent();
			foreach (NameAndPolicyRules_t nameAndPolicyRules_t in nameAndPolicyRules)
			{
				this.cmbGpoSetNames.Items.Add(nameAndPolicyRules_t.m_sName);
			}
			this.cmbGpoSetNames.SelectedIndex = 0;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00010350 File Offset: 0x0000E550
		private void cmbGpoSetNames_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = this.cmbGpoSetNames.SelectedIndex;
			NameAndPolicyRules_t nameAndPolicyRules_t = this.m_nameAndPolicyRules[selectedIndex];
			this.myRichTextBox1.Clear();
			this.myRichTextBox1.SetTabs(275);
			this.myRichTextBox1.AppendHeadline("Machine CSEs:", true);
			foreach (string text in nameAndPolicyRules_t.m_rules.m_MachineCSEs.Keys)
			{
				string text2 = nameAndPolicyRules_t.m_rules.m_MachineCSEs[text];
				this.myRichTextBox1.AppendText(text + "\t" + text2, true);
			}
			this.myRichTextBox1.AppendText(string.Empty, true);
			this.myRichTextBox1.AppendHeadline("User CSEs:", true);
			foreach (string text3 in nameAndPolicyRules_t.m_rules.m_UserCSEs.Keys)
			{
				string text4 = nameAndPolicyRules_t.m_rules.m_UserCSEs[text3];
				this.myRichTextBox1.AppendText(text3 + "\t" + text4, true);
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000335A File Offset: 0x0000155A
		private void btnCopyList_Click(object sender, EventArgs e)
		{
			this.myRichTextBox1.RTSelectAll(sender, e);
			this.myRichTextBox1.RTCopy(sender, e);
			this.myRichTextBox1.GoToTop();
		}

		// Token: 0x04000730 RID: 1840
		private NameAndPolicyRules_t[] m_nameAndPolicyRules;
	}
}
