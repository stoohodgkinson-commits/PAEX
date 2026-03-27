using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PolicyAnalyzer
{
	// Token: 0x02000073 RID: 115
	public partial class FindDlg : Form
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x000034E1 File Offset: 0x000016E1
		public FindDlg()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000034EF File Offset: 0x000016EF
		private void btnFindNext_Click(object sender, EventArgs e)
		{
			this.m_sFindText = this.txtFindText.Text;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000350F File Offset: 0x0000170F
		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000351E File Offset: 0x0000171E
		private void txtFindText_TextChanged(object sender, EventArgs e)
		{
			this.btnFindNext.Enabled = this.txtFindText.Text.Length > 0;
		}

		// Token: 0x04000753 RID: 1875
		public string m_sFindText;
	}
}
