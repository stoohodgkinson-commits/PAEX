using System;

namespace PolicyAnalyzer
{
	// Token: 0x0200005A RID: 90
	public class PolicyPath_t
	{
		// Token: 0x06000147 RID: 327 RVA: 0x000030EC File Offset: 0x000012EC
		public string ExplainTextFormatted()
		{
			return this.m_sExplainText.Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\t", "\t");
		}

		// Token: 0x040006F3 RID: 1779
		public string m_sPolConfig;

		// Token: 0x040006F4 RID: 1780
		public string m_sPolPath = string.Empty;

		// Token: 0x040006F5 RID: 1781
		public string m_sPolName;

		// Token: 0x040006F6 RID: 1782
		public string m_sPolNameSubOption;

		// Token: 0x040006F7 RID: 1783
		public string m_sExplainText = string.Empty;
	}
}
