using System;
using System.IO;

namespace PolicyAnalyzer
{
	// Token: 0x02000067 RID: 103
	public class PolicyAnalyzerDataFolder
	{
		// Token: 0x0600017C RID: 380 RVA: 0x000033A0 File Offset: 0x000015A0
		public static string DefaultSubfolderName()
		{
			return "PolicyAnalyzer";
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000033A7 File Offset: 0x000015A7
		public static string DefaultFolderPath()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), PolicyAnalyzerDataFolder.DefaultSubfolderName());
		}
	}
}
