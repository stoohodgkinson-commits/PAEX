using System;

namespace PolicyAnalyzer
{
	// Token: 0x02000069 RID: 105
	internal class ExcelColumnName
	{
		// Token: 0x06000184 RID: 388 RVA: 0x000033E5 File Offset: 0x000015E5
		public ExcelColumnName()
		{
			this.Reset();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000033F3 File Offset: 0x000015F3
		public void Reset()
		{
			this.a1 = '@';
			this.a2 = 'A';
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000108D0 File Offset: 0x0000EAD0
		public static ExcelColumnName operator ++(ExcelColumnName ecn)
		{
			char c = ecn.a2 + '\u0001';
			ecn.a2 = c;
			if (c > 'Z')
			{
				ecn.a1 += '\u0001';
				ecn.a2 = 'A';
			}
			return ecn;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00003405 File Offset: 0x00001605
		public override string ToString()
		{
			if (this.a1 == '@')
			{
				return this.a2.ToString();
			}
			return this.a1.ToString() + this.a2.ToString();
		}

		// Token: 0x04000736 RID: 1846
		private char a1;

		// Token: 0x04000737 RID: 1847
		private char a2;
	}
}
