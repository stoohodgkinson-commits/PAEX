using System;

namespace PolicyAnalyzer
{
	// Token: 0x0200005B RID: 91
	public enum eInSetResult_t
	{
		// Token: 0x040006F9 RID: 1785
		eNoSetting,
		// Token: 0x040006FA RID: 1786
		eSingleSetting,
		// Token: 0x040006FB RID: 1787
		eSingleDelete,
		// Token: 0x040006FC RID: 1788
		eDuplicateSetting,
		// Token: 0x040006FD RID: 1789
		eDuplicateDelete,
		// Token: 0x040006FE RID: 1790
		eConflictingSettings,
		// Token: 0x040006FF RID: 1791
		eError
	}
}
