using System;
using System.Collections.Generic;

namespace GPLookup
{
	// Token: 0x02000033 RID: 51
	public class PolInfo_t : ICloneable
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00002B56 File Offset: 0x00000D56
		public PolInfo_t()
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002B69 File Offset: 0x00000D69
		public PolInfo_t(PolInfo_t.class_t cls, string sKey, string sValue)
		{
			this.polClass = cls;
			this.polHive = PolInfo_t.ClassToHive(cls);
			this.polKey = sKey;
			this.polValue = sValue;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00007EC8 File Offset: 0x000060C8
		public object Clone()
		{
			PolInfo_t polInfo_t = new PolInfo_t(this.polClass, this.polKey, this.polValue);
			polInfo_t.secSettingsDetails = this.secSettingsDetails;
			foreach (PolLocation_t polLocation_t in this.polLocations.Values)
			{
				polInfo_t.polLocations.Add(polLocation_t.SortKey(), (PolLocation_t)polLocation_t.Clone());
			}
			return polInfo_t;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00007F54 File Offset: 0x00006154
		public void AddLoc(string sPolName, string sCat, string sDisp, string sDispSubOption, string sExplainText, string sFilename)
		{
			PolLocation_t polLocation_t = new PolLocation_t(sPolName, sCat, sDisp, sDispSubOption, sExplainText, sFilename);
			if (!this.polLocations.ContainsKey(polLocation_t.SortKey()))
			{
				this.polLocations.Add(polLocation_t.SortKey(), polLocation_t);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002B9D File Offset: 0x00000D9D
		public static string ClassToHive(PolInfo_t.class_t cls)
		{
			if (cls != PolInfo_t.class_t.User)
			{
				return "HKLM";
			}
			return "HKCU";
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002BAE File Offset: 0x00000DAE
		public string SortKey()
		{
			return PolInfo_t.SortKey(PolInfo_t.ClassToHive(this.polClass), this.polKey, this.polValue);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002BCC File Offset: 0x00000DCC
		public static string SortKey(string hive, string key, string value)
		{
			return string.Concat(new string[] { hive, "!", key, "!", value }).ToLower();
		}

		// Token: 0x04000127 RID: 295
		public PolInfo_t.class_t polClass;

		// Token: 0x04000128 RID: 296
		public string polHive;

		// Token: 0x04000129 RID: 297
		public string polKey;

		// Token: 0x0400012A RID: 298
		public string polValue;

		// Token: 0x0400012B RID: 299
		public SortedList<string, PolLocation_t> polLocations = new SortedList<string, PolLocation_t>();

		// Token: 0x0400012C RID: 300
		public SecSettingsDetails_t secSettingsDetails;

		// Token: 0x02000034 RID: 52
		public enum class_t
		{
			// Token: 0x0400012E RID: 302
			Machine,
			// Token: 0x0400012F RID: 303
			User,
			// Token: 0x04000130 RID: 304
			SecuritySettings,
			// Token: 0x04000131 RID: 305
			AdvAudit
		}
	}
}
