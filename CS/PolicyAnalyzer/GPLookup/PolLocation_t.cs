using System;

namespace GPLookup
{
	// Token: 0x02000031 RID: 49
	public class PolLocation_t : ICloneable
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00002A68 File Offset: 0x00000C68
		public PolLocation_t()
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002A70 File Offset: 0x00000C70
		public PolLocation_t(string sPolName, string sCat, string sDisp, string sDispSubOption, string sExplainText, string sFilename)
		{
			this.policyName = sPolName;
			this.category = sCat;
			this.dispName = sDisp;
			this.dispNameSubOption = sDispSubOption;
			this.explainText = sExplainText;
			this.filename = sFilename;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002AA5 File Offset: 0x00000CA5
		public object Clone()
		{
			return new PolLocation_t(this.policyName, this.category, this.dispName, this.dispNameSubOption, this.explainText, this.filename);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public static string SortKey(string polname, string cat, string name, string subname, string fname)
		{
			return string.Concat(new string[] { polname, "!", cat, "!", name, "!", fname }).ToLower();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002B0B File Offset: 0x00000D0B
		public string SortKey()
		{
			return PolLocation_t.SortKey(this.policyName, this.category, this.dispName, this.dispNameSubOption, this.filename);
		}

		// Token: 0x0400011F RID: 287
		public string policyName;

		// Token: 0x04000120 RID: 288
		public string category;

		// Token: 0x04000121 RID: 289
		public string dispName;

		// Token: 0x04000122 RID: 290
		public string dispNameSubOption;

		// Token: 0x04000123 RID: 291
		public string explainText;

		// Token: 0x04000124 RID: 292
		public string filename;
	}
}
