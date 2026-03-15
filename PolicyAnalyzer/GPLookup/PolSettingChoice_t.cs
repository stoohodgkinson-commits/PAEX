using System;

namespace GPLookup
{
	// Token: 0x02000032 RID: 50
	public class PolSettingChoice_t
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00002B30 File Offset: 0x00000D30
		public PolSettingChoice_t(PolLocation_t polLoc, string sValue)
		{
			this.m_PolLoc = polLoc;
			this.m_sSettingChoice = sValue;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002B46 File Offset: 0x00000D46
		public PolLocation_t PolLocation
		{
			get
			{
				return this.m_PolLoc;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002B4E File Offset: 0x00000D4E
		public string SettingChoice
		{
			get
			{
				return this.m_sSettingChoice;
			}
		}

		// Token: 0x04000125 RID: 293
		private PolLocation_t m_PolLoc;

		// Token: 0x04000126 RID: 294
		private string m_sSettingChoice;
	}
}
